using System;
using System.Collections.Generic;
using System.Linq;

namespace HW07._The_V_Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            var dictionary = new Dictionary<string, Dictionary<string, SortedSet<string>>>();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "Statistics")
                {
                    break;  
                }

                string[] line = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (line[1] == "joined")
                {
                    string username = line[0];

                    if (!dictionary.ContainsKey(username))
                    {
                        dictionary.Add(username, new Dictionary<string, SortedSet<string>>());
                        dictionary[username].Add("followed", new SortedSet<string>());
                        dictionary[username].Add("followers", new SortedSet<string>());
                    }

                }
                else if (line[1] == "followed")
                {
                    string mainUser = line[2];
                    string follower = line[0];

                    if (!dictionary.ContainsKey(mainUser) || !dictionary.ContainsKey(follower))
                    {
                        continue;
                    }

                    if (mainUser == follower)
                    {
                        continue;
                    }

                    dictionary[mainUser]["followers"].Add(follower);
                    dictionary[follower]["followed"].Add(mainUser);
                }
            }

            Console.WriteLine($"The V-Logger has a total of {dictionary.Count} vloggers in its logs.");

            var sortedDictionary = dictionary
                .OrderByDescending(x => x.Value["followers"].Count)
                .ThenBy(x => x.Value["followed"].Count)
                .ToDictionary(k => k.Key, v => v.Value);

            int counter = 1;

            foreach (var (vlogger, collectionOfPeople) in sortedDictionary)
            {
                Console.WriteLine($"{counter}. {vlogger} : {collectionOfPeople["followers"].Count} followers, {collectionOfPeople["followed"].Count} following");

                if (counter == 1)
                {
                    foreach (var username in collectionOfPeople["followers"])
                    {
                        Console.WriteLine($"*  {username}");
                    }
                }

                counter++;
            }
        }
    }
    
    
}
