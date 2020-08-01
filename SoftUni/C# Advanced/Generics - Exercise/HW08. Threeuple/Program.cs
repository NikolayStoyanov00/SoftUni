using System;

namespace Threeuple
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] firstElements = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string names = firstElements[0] + " " + firstElements[1];
            string address = firstElements[2];
            string town = firstElements[3];

            if (firstElements.Length > 4)
            {
                town += " " + firstElements[4];
            }

            Threeuple firstThreeuple = new Threeuple(names, address, town);

            string[] secondElements = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string name = secondElements[0];
            int beerLitres = int.Parse(secondElements[1]);
            string drunkOrNot = secondElements[2];

            Threeuple secondThreeuple = new Threeuple(name, beerLitres, drunkOrNot);


            string[] thirdElements = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string username = thirdElements[0];
            double bankBalance = double.Parse(thirdElements[1]);
            string bankName = thirdElements[2];

            Threeuple thirdThreeuple = new Threeuple(username, bankBalance, bankName);

            bool boolean = false;

            if (secondThreeuple.DrunkOrNot == "drunk")
            {
                boolean = true;
            }

            Console.WriteLine($"{firstThreeuple.Name} -> {firstThreeuple.Address} -> {firstThreeuple.Town}");
            Console.WriteLine($"{secondThreeuple.Name} -> {secondThreeuple.BeerLitres} -> {boolean}");
            Console.WriteLine($"{thirdThreeuple.Name} -> {thirdThreeuple.BankBalance} -> {thirdThreeuple.BankName}");
        }
    }
}
