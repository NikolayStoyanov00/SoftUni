﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _10._Explicit_Interfaces
{
    public class Citizen : IResident, IPerson
    {
        public Citizen(string name, string country, int age)
        {
            this.Name = name;
            this.Country = country;
            this.Age = age;
        }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }

        string IResident.GetName()
        {
            return "Mr/Ms/Mrs";
        }

        string IPerson.GetName()
        {
            return this.Name;
        }
    }
}