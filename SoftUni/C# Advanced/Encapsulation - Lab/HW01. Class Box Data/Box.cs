using System;
using System.Collections.Generic;
using System.Text;

namespace _01._Class_Box_Data
{
    public class Box
    {
        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        private double length;
        private double width;
        private double height;
        public double Length 
        {
            get
            {
                return this.length;
            }

            private set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Length cannot be zero or negative.");
                }

                this.length = value;
            }
        }
        public double Width
        {
            get
            {
                return this.width;
            }

            private set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Width cannot be zero or negative.");
                }

                this.width = value;
            }
        }
        public double Height
        {
            get
            {
                return this.height;
            }

            private set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Height cannot be zero or negative.");
                }

                this.height = value;
            }
        }

        public double LateralSurfaceArea()
        {
            return 2 * length * height + 2 * width * height;
        }

        public double Volume()
        {
            return length * width * height;
        }

        public double SurfaceArea()
        {
            return 2 * length * width + 2 * length * height + 2 * width * height;
        }
    }
}
