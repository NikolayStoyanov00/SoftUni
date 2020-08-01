using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyHeaven
{
    public class Restaurant
    {
        private List<Salad> data;

        public string Name { get; set; }

        public Restaurant(string name)
        {
            this.Name = name;
            data = new List<Salad>();
        }

        public void Add(Salad salad)
        {
            data.Add(salad);
        }

        public bool Buy(string name)
        {
            foreach (var salad in data)
            {
                if (salad.Name == name)
                {
                    data.Remove(salad);
                    return true;
                }
            }
            return false;
        }

        public Salad GetHealthiestSalad()
        {
            int currentMinimumCalories = int.MaxValue;
            Salad healthiestSalad = data[0];

            foreach (var salad in data)
            {
                if (salad.GetTotalCalories() < currentMinimumCalories)
                {
                    healthiestSalad = salad;
                }
            }

            return healthiestSalad;
        }

        public string GenerateMenu()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Name} have {data.Count} salads:");

            foreach (var salad in data)
            {
                sb.AppendLine(salad.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
