using System;

namespace Tuple
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] firstPersonParameters = Console.ReadLine().Split(" ");

            string firstName = firstPersonParameters[0];
            string lastName = firstPersonParameters[1];
            string address = firstPersonParameters[2];

            var firstTuple = new Tuple<string, string, string>(firstName, lastName, address);
            
            string[] secondPersonParameters = Console.ReadLine()
                .Split(" ");

            string name = secondPersonParameters[0];
            int beerAmount = int.Parse(secondPersonParameters[1]);

            var secondTuple = new Tuple<string, int>(name, beerAmount);

            string[] someParams = Console.ReadLine()
                .Split(" ");

            int integerNum = int.Parse(someParams[0]);
            double doubleNum = double.Parse(someParams[1]);

            var thirdTuple = new Tuple<int, double>(integerNum, doubleNum);

            Console.WriteLine($"{firstTuple.Item1} {firstTuple.Item2} -> {firstTuple.Item3}");
            Console.WriteLine($"{secondTuple.Item1} -> {secondTuple.Item2}");
            Console.WriteLine($"{thirdTuple.Item1} -> {thirdTuple.Item2}");

        }
    }
}
