using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Filter_By_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var pairs = new Dictionary<string, int>(); 

            for (int i = 0; i < n; i++)
            {
                string[] pair = Console.ReadLine()
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries);

                string name = pair[0];
                int age = int.Parse(pair[1]);

                pairs.Add(name, age);
            }

            string condition = Console.ReadLine();
            int ageCondition = int.Parse(Console.ReadLine());
            string format = Console.ReadLine();

            pairs.Where(x => x.Value >= 20);
            foreach (var pair in pairs)
            {
                if (ChecksIfEligible(pair, condition, ageCondition))
                {
                    PrintsCandidates(pair, format);
                }
            }
        }

        private static bool ChecksIfEligible(KeyValuePair<string, int> pair, string condition, int ageCondition)
        {
            if (condition == "older")
            {
                if (pair.Value >= ageCondition)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (pair.Value < ageCondition)
                {
                    return true;
                }
                return false;
            }
        }

        private static void PrintsCandidates(KeyValuePair<string, int> pair, string format)
        {
            if (format == "name age")
            {
                Console.WriteLine($"{pair.Key} - {pair.Value}");
            }
            else if (format == "name")
            {
                Console.WriteLine(pair.Key);
            }
            else if (format == "age")
            {
                Console.WriteLine(pair.Value);
            }
        }

    }
}
