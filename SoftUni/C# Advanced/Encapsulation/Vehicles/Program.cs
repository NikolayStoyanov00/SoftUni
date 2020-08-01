using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.WildFarm
{
    public abstract class Food
    {
        private int quantity;

        protected Food(int quantity)
        {
            this.Quantity = quantity;
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }
    }

    public abstract class Animal
    {
        private string animalName;
        private string animalType;
        private double animalWeight;
        private int foodEaten;

        protected Animal(string animalName, string animalType, double animalWeight)
        {
            this.AnimalName = animalName;
            this.AnimalType = animalType;
            this.AnimalWeight = animalWeight;
            this.FoodEaten = foodEaten;
        }

        public string AnimalName
        {
            get
            {
                return animalName;
            }
            set
            {
                animalName = value;
            }
        }

        public string AnimalType
        {
            get
            {
                return animalType;
            }
            set
            {
                animalType = value;
            }
        }

        public double AnimalWeight
        {
            get
            {
                return animalWeight;
            }
            set
            {
                animalWeight = value;
            }
        }

        public int FoodEaten
        {
            get
            {
                return foodEaten;
            }
            set
            {
                foodEaten = value;
            }
        }

        public abstract void MakeSound();

        public abstract void Eat(Food food);
    }

    public abstract class Mammal : Animal
    {
        private string livingRegion;

        protected Mammal(string animalName, string animalType, double animalWeight, string livingRegion)
            : base(animalName, animalType, animalWeight)
        {
            this.LivingRegion = livingRegion;
        }

        public string LivingRegion
        {
            get
            {
                return livingRegion;
            }
            set
            {
                livingRegion = value;
            }
        }

        public override string ToString()
        {
            return $"{this.AnimalType}[{this.AnimalName}, {this.AnimalWeight}, {this.LivingRegion}, {this.FoodEaten}]";
        }

    }

    public class Mouse : Mammal
    {
        public Mouse(string animalName, string animalType, double animalWeight, string livingRegion)
            : base(animalName, animalType, animalWeight, livingRegion)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine("SQUEEEAAAK!");
        }

        public override void Eat(Food food)
        {
            if (food.GetType().Name != "Vegetable")
            {
                Console.WriteLine($"{this.AnimalType}s are not eating that type of food!");
            }
            else
            {
                this.FoodEaten += food.Quantity;
                Console.WriteLine("A cheese was just eaten!");
            }
        }

    }

    public class Zebra : Mammal
    {
        public Zebra(string animalName, string animalType, double animalWeight, string livingRegion)
            : base(animalName, animalType, animalWeight, livingRegion)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine("Zs");
        }

        public override void Eat(Food food)
        {
            if (food.GetType().Name != "Vegetable")
            {
                Console.WriteLine($"{this.AnimalType}s are not eating that type of food!");
            }
            else
            {
                this.FoodEaten += food.Quantity;
            }
        }
    }

    public abstract class Felime : Mammal
    {
        public Felime(string animalName, string animalType, double animalWeight, string livingRegion)
            : base(animalName, animalType, animalWeight, livingRegion)
        {

        }
    }

    public class Cat : Felime
    {
        private string breed;

        public Cat(string animalName, string animalType, double animalWeight, string livingRegion, string breed)
            : base(animalName, animalType, animalWeight, livingRegion)
        {
            this.Breed = breed;
        }

        public string Breed
        {
            get
            {
                return breed;
            }
            set
            {
                breed = value;
            }
        }

        public override void MakeSound()
        {
            Console.WriteLine("Meowwww");
        }

        public override void Eat(Food food)
        {
            this.FoodEaten += food.Quantity;
        }

        public override string ToString()
        {
            return $"{this.AnimalType}[{this.AnimalName}, {this.Breed}, {this.AnimalWeight}, {this.LivingRegion}, {this.FoodEaten}]";
        }
    }

    public class Tiger : Felime
    {
        public Tiger(string animalName, string animalType, double animalWeight, string livingRegion)
            : base(animalName, animalType, animalWeight, livingRegion)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine("ROAAR!!!");
        }

        public override void Eat(Food food)
        {
            if (food.GetType().Name != "Meat")
            {
                Console.WriteLine($"{this.AnimalType}s are not eating that type of food!");
            }
            else
            {
                this.FoodEaten += food.Quantity;
            }
        }
    }

    public class Vegetable : Food
    {
        public Vegetable(int quantity)
            : base(quantity)
        {
        }
    }

    public class Meat : Food
    {
        public Meat(int quantity)
            : base(quantity)
        {
        }
    }

    public class StartUp
    {
        public static void Main()
        {
            string animalInformation = Console.ReadLine();

            while (animalInformation != "End")
            {
                var foodForEat = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var typeFood = foodForEat[0];
                var foodQuantity = int.Parse(foodForEat[1]);

                Food currentFood = null;

                if (typeFood == "Vegetable")
                {
                    currentFood = new Vegetable(foodQuantity);
                }
                else
                {
                    currentFood = new Meat(foodQuantity);
                }


                var animalTokens = animalInformation
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var animalType = animalTokens[0];
                var animalName = animalTokens[1];
                var animalWeight = double.Parse(animalTokens[2]);
                var livingRegion = animalTokens[3];

                Animal currentAnimal = null;

                if (animalTokens.Length == 5)
                {
                    var catBreed = animalTokens[4];
                    currentAnimal = new Cat(animalName, animalType, animalWeight, livingRegion, catBreed);
                }
                else
                {
                    switch (animalType)
                    {
                        case "Tiger":
                            currentAnimal = new Tiger(animalName, animalType, animalWeight, livingRegion);
                            break;
                        case "Zebra":
                            currentAnimal = new Zebra(animalName, animalType, animalWeight, livingRegion);
                            break;
                        case "Mouse":
                            currentAnimal = new Mouse(animalName, animalType, animalWeight, livingRegion);
                            break;
                    }
                }

                currentAnimal.MakeSound();
                currentAnimal.Eat(currentFood);
                Console.WriteLine(currentAnimal);

                animalInformation = Console.ReadLine();
            }
        }
    }
}