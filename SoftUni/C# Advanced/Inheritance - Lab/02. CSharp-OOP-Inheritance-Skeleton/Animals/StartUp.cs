using System;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();
            while (true)
            {
                string command = Console.ReadLine();

                if (command == "Beast!")
                {
                    break;
                }

                string animalType = command;

                if (animalType == "Cat")
                {
                    string[] animalParams = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    string name = animalParams[0];
                    int age = int.Parse(animalParams[1]);
                    string gender = animalParams[2];

                    Cat cat = new Cat(name, age, gender);
                    animals.Add(cat);
                }
                else if (animalType == "Kitten")
                {
                    string[] animalParams = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    string name = animalParams[0];
                    int age = int.Parse(animalParams[1]);
                    string gender = animalParams[2];

                    Kitten cat = new Kitten(name, age);
                    animals.Add(cat);
                }

                else if (animalType == "Tomcat")
                {
                    string[] animalParams = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    string name = animalParams[0];
                    int age = int.Parse(animalParams[1]);
                    string gender = animalParams[2];

                    Tomcat cat = new Tomcat(name, age);
                    animals.Add(cat);
                }

                else if (animalType == "Frog")
                {
                    string[] animalParams = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    string name = animalParams[0];
                    int age = int.Parse(animalParams[1]);
                    string gender = animalParams[2];

                    Frog frog = new Frog(name, age, gender);
                    animals.Add(frog);
                }

                else if (animalType == "Dog")
                {
                    string[] animalParams = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    string name = animalParams[0];
                    int age = int.Parse(animalParams[1]);
                    string gender = animalParams[2];

                    Dog dog = new Dog(name, age, gender);
                    animals.Add(dog);
                }
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
