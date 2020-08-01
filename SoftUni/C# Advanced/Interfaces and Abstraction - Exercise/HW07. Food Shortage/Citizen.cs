using System;
using System.Collections.Generic;
using System.Text;

namespace _07._Food_Shortage
{
    public class Citizen : IBirthdate, IBuyer
    {
        public Citizen(string name, int age, string id, string birthdate)
        {
            this.Name = name;
            this.Age = age;
            this.Id = id;
            this.Birthdate = birthdate;
        }
        public string Name { get; private set; }

        public int Age { get; private set; }

        public string Id { get; private set; }

        public string Birthdate { get; set; }

        public int Food { get; set; } = 0;

        public void BuyFood()
        {
            this.Food += 10;
        }
    }
}
