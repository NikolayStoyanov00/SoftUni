using System;
using System.Collections.Generic;
using System.Text;

namespace _03._Wild_Farm.Animal
{
    public abstract class Animal
    {
        public Animal (string type, string name, double weight, int foodEaten)
        {
            this.Type = type;
            this.Name = name;
            this.Weight = weight;
            this.FoodEaten = foodEaten;
        }
        public string Name { get; set; }

        public double Weight { get; set; }

        public int FoodEaten { get; set; }

        public string Type { get; set; }
    }
}
