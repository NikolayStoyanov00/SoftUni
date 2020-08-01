using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Person(string name, int age)
        {
            this.Name = name;

            if (age < 0)
            {
                return;
            }

            this.Age = age;
        }

        public override string ToString()
        {
            return $"Name: {this.Name}, Age: {this.Age}";
        }
    }
}
