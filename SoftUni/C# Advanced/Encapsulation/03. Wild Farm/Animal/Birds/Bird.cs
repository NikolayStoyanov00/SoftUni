using System;
using System.Collections.Generic;
using System.Text;

namespace _03._Wild_Farm.Animal.Birds
{
    public class Bird : Animal
    {
        public Bird(string type, string name, double weight, int foodEaten, double wingSize)
            :base (type, name, weight, foodEaten)
        {
            this.WingSize = wingSize;
        }

        public double WingSize { get; set; }
    }
}
