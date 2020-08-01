using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Wild_Farm
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Animal.Animal> animals = new List<Animal.Animal>();
            int counter = 0;
            int vegetablesCounter = 0;
            while (true)
            {
                string command = Console.ReadLine();

                if (command == "End")
                {
                    break;
                }

                string[] parameters = command
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (counter % 2 == 0)
                {
                    string type = parameters[0];
                    string name = parameters[1];
                    double weight = double.Parse(parameters[2]);

                    if (type == "Cat" || type == "Tiger")
                    {
                        string livingRegion = parameters[3];
                        string breed = parameters[4];

                        if (type == "Cat")
                        {
                            Animal.Mammals.Feline.Cat cat = new Animal.Mammals.Feline.Cat(type, name, weight, 0, livingRegion, breed);
                            animals.Add(cat);
                            Console.WriteLine(cat);
                        }
                        else if (type == "Tiger")
                        {
                            Animal.Mammals.Feline.Tiger tiger = new Animal.Mammals.Feline.Tiger(type, name, weight, 0, livingRegion, breed);
                            animals.Add(tiger);
                            Console.WriteLine(tiger);
                        }
                    }
                    else if (type == "Hen" || type == "Owl")
                    {
                        double wingSize = double.Parse(parameters[3]);

                        if (type == "Hen")
                        {
                            Animal.Birds.Hen hen = new Animal.Birds.Hen(type, name, weight, 0, wingSize);
                            animals.Add(hen);
                            Console.WriteLine(hen);
                        }
                        else if (type == "Owl")
                        {
                            Animal.Birds.Owl owl = new Animal.Birds.Owl(type, name, weight, 0, wingSize);
                            animals.Add(owl);
                            Console.WriteLine(owl);
                        }
                    }
                    else if (type == "Mouse" || type == "Dog")
                    {
                        string livingRegion = parameters[3];

                        if (type == "Mouse")
                        {
                            Animal.Mammals.Mouse mouse = new Animal.Mammals.Mouse(type, name, weight, 0, livingRegion);
                            animals.Add(mouse);
                            Console.WriteLine(mouse);
                        }
                        else if (type == "Dog")
                        {
                            Animal.Mammals.Dog dog = new Animal.Mammals.Dog(type, name, weight, 0, livingRegion);
                            animals.Add(dog);
                            Console.WriteLine(dog);
                        }
                    }
                }
                else if (counter % 2 == 1)
                {
                    vegetablesCounter++;
                    string foodType = parameters[0];
                    int quantity = int.Parse(parameters[1]);

                    string animalType = animals[vegetablesCounter - 1].Type;
                    if (animalType == "Hen")
                    {
                        Animal.Birds.Hen hen = (Animal.Birds.Hen)animals[vegetablesCounter - 1];
                        hen.FoodEaten += quantity;
                        hen.Weight += 0.35 * quantity;
                    }

                    else if (animalType == "Mouse")
                    {
                        if (foodType == "Vegetable" || foodType == "Fruit")
                        {
                            Animal.Mammals.Mouse mouse = (Animal.Mammals.Mouse)animals[vegetablesCounter - 1];
                            mouse.FoodEaten += quantity;
                            mouse.Weight += 0.10 * quantity;
                        }
                        else
                        {
                            Console.WriteLine($"Mice does not eat {foodType}");
                        }
                    }

                    else if (animalType == "Cat")
                    {
                        if (foodType == "Vegetable" || foodType == "Meat")
                        {
                            Animal.Mammals.Feline.Cat cat = (Animal.Mammals.Feline.Cat)animals[vegetablesCounter - 1];
                            cat.FoodEaten += quantity;
                            cat.Weight += 0.30 * quantity;
                        }
                        else
                        {
                            Console.WriteLine($"{animalType} does not eat {foodType}");
                        }
                    }

                    else if (animalType == "Tiger"
                        || animalType == "Dog"
                        || animalType == "Owl")
                    {
                        if (foodType == "Meat")
                        {
                            if (animalType == "Tiger")
                            {
                                Animal.Mammals.Feline.Tiger tiger = (Animal.Mammals.Feline.Tiger)animals[vegetablesCounter - 1];
                                tiger.FoodEaten += quantity;
                                tiger.Weight += 1.00 * quantity;
                            }

                            else if (animalType == "Dog")
                            {
                                Animal.Mammals.Dog dog = (Animal.Mammals.Dog)animals[vegetablesCounter - 1];
                                dog.FoodEaten += quantity;
                                dog.Weight += 0.40 * quantity;
                            }

                            else if (animalType == "Owl")
                            {
                                Animal.Birds.Owl owl = (Animal.Birds.Owl)animals[vegetablesCounter - 1];
                                owl.FoodEaten += quantity;
                                owl.Weight += 0.25 * quantity;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{animalType} does not eat {foodType}!");
                        }
                    }
                }
                counter++;
            }

            foreach (Animal.Animal animal in animals)
            {
                string animalType = animal.Type;

                if (animalType == "Hen" || animalType == "Owl")
                {
                    Animal.Birds.Bird bird = (Animal.Birds.Bird)animal;

                    if (animalType == "Hen")
                    {
                        Console.WriteLine($"Hen [{bird.Name}, {bird.WingSize}, {bird.Weight}, {bird.FoodEaten}]");
                    }
                    else
                    {
                        Console.WriteLine($"Owl [{bird.Name}, {bird.WingSize}, {bird.Weight}, {bird.FoodEaten}]");
                    }
                }
                else if (animalType == "Cat" || animalType == "Tiger")
                {
                    Animal.Mammals.Feline.Feline feline = (Animal.Mammals.Feline.Feline)animal;

                    if (animalType == "Cat")
                    {
                        Console.WriteLine($"Cat [{feline.Name}, {feline.Breed}, {feline.Weight}, {feline.LivingRegion}, {feline.FoodEaten}]");
                    }
                    else
                    {
                        Console.WriteLine($"Tiger [{feline.Name}, {feline.Breed}, {feline.Weight}, {feline.LivingRegion}, {feline.FoodEaten}]");
                    }
                }
                else if (animalType == "Mouse" || animalType == "Dog")
                {
                    Animal.Mammals.Mammal mammal = (Animal.Mammals.Mammal)animal;

                    if (animalType == "Mouse")
                    {
                        Console.WriteLine($"Mouse [{mammal.Name}, {mammal.Weight}, {mammal.LivingRegion}, {mammal.FoodEaten}]");
                    }
                    else
                    {
                        Console.WriteLine($"Dog [{mammal.Name}, {mammal.Weight}, {mammal.LivingRegion}, {mammal.FoodEaten}]");
                    }
                }
            }
        }
    }
}
