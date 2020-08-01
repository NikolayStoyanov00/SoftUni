using ProductShop.Data;
using ProductShop.Dtos.Export;
using ProductShop.Dtos.Import;
using ProductShop.Models;
using ProductShop.XmlHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ProductShop
{
    public class StartUp
    {
        public const string path = "../../../Exported";
        public static void Main(string[] args)
        {
            ProductShopContext context = new ProductShopContext();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var resultProducts = GetUsersWithProducts(context);

            Console.WriteLine(resultProducts);

            File.WriteAllText(path + "/users-and-products.xml", resultProducts);
        }

        private static void ResetDatabase(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted");

            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created");
        }

        //01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            const string rootName = "Users";
            var usersResult = XmlConverter.Deserializer<ImportUserDto>(inputXml, rootName);

            var users = usersResult
                .Select(u => new User { FirstName = u.FirstName, LastName = u.LastName, Age = u.Age })
                .ToList();

            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count}";
        }

        //02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            const string rootName = "Products";
            var productsResult = XmlConverter.Deserializer<ImportProductDto>(inputXml, rootName);

            var products = productsResult
                .Select(x => new Product { Name = x.Name, Price = x.Price, SellerId = x.SellerId, BuyerId = x.BuyerId })
                .ToArray();
            context.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        //03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            const string rootName = "Categories";

            var categoriesResult = XmlConverter.Deserializer<ImportCategoryDto>(inputXml, rootName);

            var categories = categoriesResult
                .Where(x => x.Name != null)
                .Select(x => new Category { Name = x.Name })
                .ToList();

            context.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        //04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            const string rootName = "CategoryProducts";

            var categoriesProductsResult = XmlConverter.Deserializer<ImportCategoryProductDto>(inputXml, rootName);

            var categoriesProducts = new List<CategoryProduct>();

            foreach (var categoryProduct in categoriesProductsResult)
            {
                if (context.Categories.Any(x => x.Id == categoryProduct.CategoryId) &&
                    context.Products.Any(x => x.Id == categoryProduct.ProductId))
                {
                    categoriesProducts.Add(new CategoryProduct 
                    { 
                        CategoryId = categoryProduct.CategoryId, ProductId = categoryProduct.ProductId 
                    });
                }
            }

            context.CategoryProducts.AddRange(categoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Count}";
        }

        //05. Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            const string rootName = "Products";

            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ExportProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    BuyerFullName = p.Buyer.FirstName + " " + p.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToList();

            var resultXml = XmlConverter.Serialize(products, rootName);

            return resultXml;
        }

        //06. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            const string rootName = "Users";

            var users = context
                .Users
                .Where(u => u.ProductsSold.Count >= 1)
                .Select(u => new ExportUserWithSoldProductsDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    soldProducts = u.ProductsSold.Select(p => new ProductDto { Name = p.Name, Price = p.Price }).ToList()
                })
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ToList();

            var resultXml = XmlConverter.Serialize(users, rootName);

            return resultXml;
        }

        //07. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            const string rootName = "Categories";

            var categories = context
                .Categories
                .Select(c => new ExportCategoryByProductsDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(c => c.TotalRevenue)
                .ToList();

            var resultXml = XmlConverter.Serialize(categories, rootName);

            return resultXml;
        }

        //08. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            const string rootName = "Users";

            var users = context
                .Users
                .ToArray()
                .Where(u => u.ProductsSold.Any())
                .Select(u => new UserInfo
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductCount
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold.Select(ps => new SoldProduct
                        {
                            Name = ps.Name,
                            Price = ps.Price
                        })
                        .OrderByDescending(ps => ps.Price)
                        .ToList()
                    }
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .Take(10)
                .ToList();

            var finalObj = new ExportUserCountDto
            {
                Count = context.Users.Count(x => x.ProductsSold.Any()),
                Users = users
            };

            var resultXml = XmlConverter.Serialize(finalObj, rootName);

            return resultXml;
        }
    }
}