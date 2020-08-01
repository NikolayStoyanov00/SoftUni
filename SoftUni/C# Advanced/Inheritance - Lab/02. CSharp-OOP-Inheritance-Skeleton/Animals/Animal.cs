using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public class Animal
    {
        private int age;
        private string gender;
        public Animal(string name, int age, string gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }
        public string Name { get; set; }

        public int Age {
            get
            {
                return this.age;
            }
                
            set
            {
                if (value > 0)
                {
                    this.age = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public string Gender 
        {
            get
            {
                return this.gender;
            }
            set
            {
                if (value != "Male" && value != "Female")
                {
                    throw new ArgumentException();
                }
                else
                {
                    this.gender = value;
                }
            }
        }

        public virtual string ProduceSound()
        {
            return "";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{GetType().Name}");
            sb.AppendLine($"{this.Name} {this.Age} {this.Gender} ");
            sb.Append($"{ProduceSound()}");

            return sb.ToString().Trim();
        }
    }
}
