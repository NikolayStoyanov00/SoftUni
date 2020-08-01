using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{ 
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Family family = new Family();
            family.People = new System.Collections.Generic.List<Person>();
            for (int i = 0; i < n; i++)
            {
                string[] personParams = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string personName = personParams[0];
                int personAge = int.Parse(personParams[1]);

                Person person = new Person(personName, personAge);
                family.AddMember(person);
            }

            List<Person> updatedPeople = new List<Person>();

            for (int i = 0; i < family.People.Count; i++)
            {
                if (family.People[i].Age > 30)
                {
                    updatedPeople.Add(family.People[i]);
                }
            }

            foreach (var person in updatedPeople.OrderBy(x => x.Name))
            {
                Console.WriteLine($"{person.Name} - {person.Age}");
            }
        }
    }
}
