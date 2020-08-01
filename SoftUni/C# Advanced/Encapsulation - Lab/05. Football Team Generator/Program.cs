using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Football_Team_Generator
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();
            while (true)
            {
                try
                {
                    string command = Console.ReadLine();

                    if (command == "END")
                    {
                        break;
                    }

                    string[] tokens = command.Split(";", StringSplitOptions.RemoveEmptyEntries);

                    if (tokens[0] == "Team")
                    {
                        string teamName = tokens[1];
                        Team team = new Team(teamName);

                        teams.Add(team);
                    }

                    if (tokens[0] == "Add")
                    {
                        string teamName = tokens[1];

                        Team team = teams.FirstOrDefault(x => x.Name == teamName);
                        if (team == null)
                        {
                            Console.WriteLine($"Team {teamName} does not exist.");
                        }
                        else
                        {
                            string playerName = tokens[2];
                            int enduranceStat = int.Parse(tokens[3]);
                            int sprintStat = int.Parse(tokens[4]);
                            int dribbleStat = int.Parse(tokens[5]);
                            int passingStat = int.Parse(tokens[6]);
                            int shootingStat = int.Parse(tokens[7]);

                            PlayerStats.Stats stats = new PlayerStats.Stats(enduranceStat, sprintStat, dribbleStat, passingStat, shootingStat);
                            Player player = new Player(playerName, stats);
                            team.AddPlayer(player);
                        }
                    }

                    if (tokens[0] == "Remove")
                    {
                        string teamName = tokens[1];
                        string playerName = tokens[2];

                        Team team = teams.FirstOrDefault(x => x.Name == teamName);
                        team.RemovePlayer(playerName);
                    }

                    if (tokens[0] == "Rating")
                    {
                        string teamName = tokens[1];
                        Team team = teams.FirstOrDefault(x => x.Name == teamName);

                        Console.WriteLine($"{teamName} - {team.Rating}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
