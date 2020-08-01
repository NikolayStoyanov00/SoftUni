using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        private static string ResultDirectoryPath = @"../../../Datasets/Results";
        public static void Main(string[] args)
        {
            ProductShopContext db = new ProductShopContext();

            string inputJson = File.ReadAllText(@"../../../Datasets/categories.json");

            string result = GetUsersWithProducts(db);

            EnsureDirectoryExists(ResultDirectoryPath);
            
            File.WriteAllText(ResultDirectoryPath + "/users-and-products.json", result);
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void ResetDatabase(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted");

            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created");
        }

        //01. Import Users
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            List<User> users = JsonConvert.DeserializeObject<List<User>>(inputJson);
            context.Users.AddRange(users);
            context.SaveChanges();
            return $"Successfully imported {users.Count}";
        }

        //02. Import Products
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(inputJson);
            context.Products.AddRange(products);
            context.SaveChanges();
            return $"Successfully imported {products.Count}";
        }

        //03. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            List<Category> categories = JsonConvert
                .DeserializeObject<List<Category>>(inputJson);

            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].Name == null)
                {
                    categories.RemoveAt(i);
                }
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();
            return $"Successfully imported {categories.Count}";
        }

        //04. Import Categories and Products
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            List<CategoryProduct> categoryProducts = JsonConvert
                .DeserializeObject<List<CategoryProduct>>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();
            return $"Successfully imported {categoryProducts.Count}";
        }

        //05. Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName
                })
                .ToList();

            string json = JsonConvert.SerializeObject(products, Formatting.Indented);

            return json;
        }

        //06. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(x => x.ProductsSold.Any(y => y.Buyer != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold.Where(p => p.Buyer != null).Select(b => new
                    {
                        name = b.Name,
                        price = b.Price,
                        buyerFirstName = b.Buyer.FirstName,
                        buyerLastName = b.Buyer.LastName
                    })
                })
                .OrderBy(u => u.lastName)
                .ThenBy(u => u.firstName)
                .ToList();

            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            return json;
        }

        //07. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    averagePrice = c.CategoryProducts.Average(cp => cp.Product.Price).ToString("F"),
                    totalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price).ToString("F")
                })
                .OrderByDescending(c => c.productsCount)
                .ToList();

            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            return json;
        }

        //08. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .AsEnumerable()
                .Where(x => x.ProductsSold.Any(p => p.Buyer != null))
                .OrderByDescending(x => x.ProductsSold.Count(b => b.Buyer != null))
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    age = x.Age,
                    soldProducts = new
                    {
                        count = x.ProductsSold.Count(b => b.Buyer != null),
                        products = x.ProductsSold.Where(b => b.Buyer != null).Select(ps => new
                        {
                            name = ps.Name,
                            price = ps.Price
                        }).ToList()
                    }
                })
                .ToList();

            var usersResultObject = new
            {
                usersCount = users.Count,
                users = users
            };

            string json = JsonConvert.SerializeObject(usersResultObject, 
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,

                    NullValueHandling = NullValueHandling.Ignore
                });
            return json;
        }
    }
}