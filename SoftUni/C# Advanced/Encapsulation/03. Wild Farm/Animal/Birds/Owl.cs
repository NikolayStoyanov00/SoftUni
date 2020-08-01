using System;
using System.Collections.Generic;
using System.Text;

namespace _03._Wild_Farm.Animal.Birds
{
    public class Owl : Bird
    {
        public Owl(string type, string name, double weight, int foodEaten, double wingSize)
            : base(type, name, weight, foodEaten, wingSize)
        {
        }

        public override string ToString()
        {
            return "Hoot Hoot";
        }
    }
}
