using System;
using System.Collections.Generic;
using System.Linq;

namespace __Product_Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<string, List<Product>> dictionary = new SortedDictionary<string, List<Product>>();

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "Revision")
                {
                    break;
                }

                string[] line = command.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                string shopName = line[0];
                string productName = line[1];
                double productPrice = double.Parse(line[2]);
                Product product = new Product(productName, productPrice);
                List<Product> products = new List<Product>();
                products.Add(product);
                if (dictionary.ContainsKey(shopName))
                {
                    dictionary[shopName].Add(product);
                }
                else
                {
                    dictionary.Add(shopName, products);
                }
                products = new List<Product>();
            }

            foreach (var shop in dictionary)
            {
                Console.WriteLine($"{shop.Key}->");

                for (int i = 0; i < shop.Value.Count; i++)
                {
                    Console.WriteLine($"Product: {shop.Value[i].ProductName}, Price: {shop.Value[i].ProductPrice}");
                }
            }

        }
    }

    class Product
    {
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }

        public Product(string productName, double productPrice)
        {
            this.ProductName = productName;
            this.ProductPrice = productPrice;
        }
    }
}
