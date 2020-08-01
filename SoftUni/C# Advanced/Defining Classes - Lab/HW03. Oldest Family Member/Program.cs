using System;

namespace DefiningClasses
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Family family = new Family();
            family.People = new System.Collections.Generic.List<Person>();

            for (int i = 0; i < n; i++)
            {
                string[] personParams = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = personParams[0];
                int age = int.Parse(personParams[1]);

                Person person = new Person(name, age);

                family.AddMember(person);
            }

            Person oldestMember = family.GetOldestMember();

            Console.WriteLine($"{oldestMember.Name} {oldestMember.Age}");
        }
    }
}
