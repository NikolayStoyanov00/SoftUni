using System;
using System.Collections.Generic;
using System.Text;

namespace _03._Wild_Farm.Animal.Mammals
{
    public class Mouse : Mammal
    {
        public Mouse(string type, string name, double weight, int foodEaten, string livingRegion)
            :base(type, name, weight, foodEaten, livingRegion)
        {
        }

        public override string ToString()
        {
            return "Squeak";
        }
    }
}
