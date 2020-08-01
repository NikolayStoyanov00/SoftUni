using System;
using System.Collections.Generic;
using System.Text;

namespace _03._Ferrari
{
    public class Ferrari : ICar
    {
        public Ferrari(string driver)
        {
            this.Driver = driver;
        }
        public string Model => "488-Spider";

        public string Driver { get; set; }

        public string Gas()
        {
            return "Gas!";
        }

        public string Brake()
        {
            return "Brakes!";
        }

        public override string ToString()
        {
            return $"{this.Model}/{Brake()}/{Gas()}/{this.Driver}";
        }
    }
}
