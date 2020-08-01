﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyHeaven
{
    public class Salad
    {
        private List<Vegetable> products;
        public string Name { get; set; }

        public Salad(string name)
        {
            this.Name = name;
            products = new List<Vegetable>();
        }

        public int GetTotalCalories()
        {
            int sum = 0;

            foreach (var product in products)
            {
                sum += product.Calories;
            }

            return sum;
        }

        public int GetProductCount()
        {
            return products.Count;
        }

        public void Add(Vegetable product)
        {
            products.Add(product);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"* Salad {Name} is {GetTotalCalories()} calories and have {products.Count} products:");
            foreach (var product in products)
            {
                sb.AppendLine(product.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
