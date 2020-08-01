using System;
using System.Collections.Generic;
using System.Text;

namespace HW8._Car_Salesman
{
    public class Car
    {
        public string Model { get; set; }

        public Engine Engine { get; set; }

        public int Weight { get; set; }

        public string Color { get; set; }

        public Car(string model, Engine engine)
        {
            this.Model = model;
            this.Engine = engine;
        }

        public Car(string model, Engine engine, int weight)
            : this(model, engine)
        {
            this.Weight = weight;
        }

        public Car(string model, Engine engine, string color)
            : this(model, engine)
        {
            this.Color = color;
        }

        public Car (string model, Engine engine, int weight, string color)
            : this (model, engine)
        {
            this.Weight = weight;
            this.Color = color;
        }
    }
}
