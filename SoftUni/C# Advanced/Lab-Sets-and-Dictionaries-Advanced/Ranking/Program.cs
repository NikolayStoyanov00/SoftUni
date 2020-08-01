using System;
using System.Collections.Generic;
using System.Linq;

namespace HW08._Ranking
{
    class Program
    {
        static void Main(string[] args)
        {
            var contests = new Dictionary<string, string>();
            var users = new Dictionary<string, Dictionary<string, int>>();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "end of contests")
                {
                    break;  
                }

                string[] line = input.Split(":", StringSplitOptions.RemoveEmptyEntries);

                string contestName = line[0];
                string contestPassword = line[1];

                contests.Add(contestName, contestPassword);
            }

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "end of submissions")
                {
                    break;
                }

                string[] line = input
                    .Split("=>", StringSplitOptions.RemoveEmptyEntries);

                string contestName = line[0];
                string contestPassword = line[1];
                string username = line[2];
                int points = int.Parse(line[3]);

                if (contests.ContainsKey(contestName))
                {
                    if (contests[contestName] == contestPassword)
                    {
                        if (!users.ContainsKey(username))
                        {
                            users.Add(username, new Dictionary<string, int>());
                        }

                        if (!users[username].ContainsKey(contestName))
                        {
                            users[username].Add(contestName, points);
                        }
                        if (users[username][contestName] < points)
                        {
                            users[username][contestName] = points;
                        }
                    }
                }
            }

            foreach (var (username, contestNamesAndPoints) in users.OrderByDescending(x => x.Value.Values.Sum()).ToDictionary(x => x.Key, x => x.Value))
            {
                Console.WriteLine($"Best candidate is {username} with total {contestNamesAndPoints.Values.Sum()} points.");
                break;
            }

            Console.WriteLine("Ranking:");
            foreach (var (username, contestNamesAndPoints) in users.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{username}");

                foreach (var (currentContest, currentPoints) in contestNamesAndPoints.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"#  {currentContest} -> {currentPoints}");
                }
            }
        }
    }
}
