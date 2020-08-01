using System;
using System.Collections.Generic;
using System.Text;

namespace HW07._Raw_Data
{
    public class Tire
    {
        public double Pressure { get; set; }

        public int Age { get; set; }

        public Tire (double pressure, int age)
        {
            this.Pressure = pressure;
            this.Age = age;
        }
    }
}
