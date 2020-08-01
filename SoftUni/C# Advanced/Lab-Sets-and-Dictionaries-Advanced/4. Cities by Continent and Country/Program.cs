using System;
using System.Collections.Generic;

namespace _4._Cities_by_Continent_and_Country
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, List<string>>> dictionary = new Dictionary<string, Dictionary<string, List<string>>>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] data = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string continent = data[0];
                string country = data[1];
                string city = data[2];
                List<string> cities = new List<string>();
                cities.Add(city);
                Dictionary<string, List<string>> countryAndCities = new Dictionary<string, List<string>>();

            }

            foreach (var continent in dictionary)
            {
                Console.WriteLine($"{continent}: ");

                foreach (var country in dictionary)
                {
                    Console.WriteLine($"{country.Key} -> {string.Join(", ", country.Value)}");
                }
            }

        }
    }
}
