using System;
using System.Collections.Generic;
using System.Text;

namespace _03._Wild_Farm.Animal.Mammals.Feline
{
    public class Tiger : Feline
    {
        public Tiger(string type, string name, double weight, int foodEaten, string livingRegion, string breed)
            :base(type, name, weight, foodEaten, livingRegion, breed)
        {
        }

        public override string ToString()
        {
            return "ROAR!!!";
        }
    }
}
