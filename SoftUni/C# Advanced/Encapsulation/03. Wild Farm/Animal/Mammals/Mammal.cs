using System;
using System.Collections.Generic;
using System.Text;

namespace _03._Wild_Farm.Animal.Mammals
{
    public class Mammal : Animal
    {
        public Mammal(string type, string name, double weight, int foodEaten, string livingRegion)
            :base(type, name, weight, foodEaten)
        {
            this.LivingRegion = livingRegion;
        }
        public string LivingRegion { get; set; }
    }
}
