﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _1._Car
{
    public class Car
    {
        private string make;
        private string model;
        private int year;
        public string Make { get; set; }

        public string Model { get; set; }
        public int Year { get; set; }

        public Car()
        {
            this.Make = make;
            this.Model = model;
            this.Year = year;
        }
    }
}
