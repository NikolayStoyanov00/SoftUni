using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public class Coffee : HotBeverage
    {
        public Coffee(string name, double caffeine)
            :base(name, coffeePrice, coffeeMililiters)
        {
            this.Name = name;
            this.Caffeine = caffeine;
        }
        private const double coffeeMililiters = 50;

        private const decimal coffeePrice = 3.50m;

        public double Caffeine { get; set; }
    }
}
