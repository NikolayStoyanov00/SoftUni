using System;
using System.Collections.Generic;
using System.Text;

namespace Cars
{
    public class Tesla : ICar, IElectricCar
    {
        public Tesla(string model, string color, int battery)
        {
            this.Model = model;
            this.Color = color;
            this.Battery = battery;
        }

        private int battery;

        public int Battery
        {
            get { return battery; }
            set { battery = value; }
        }


        private string model;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Start()
        {
            return "Engine start";
        }

        public string Stop()
        {
            return "Breaaak!";
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Color} {nameof(Seat)} {this.Model} with {this.Battery} Batteries");
            sb.AppendLine(Start());
            sb.AppendLine(Stop());

            return sb.ToString().TrimEnd();
        }

    }
}
