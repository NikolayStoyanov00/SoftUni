using System;
using System.Collections.Generic;
using System.Linq;

namespace HW11._The_Party_Reservation_Filter_Module
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> names = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();


            var filters = new List<string>();

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "Print")
                {
                    break;  
                }

                string[] tokens = command.Split(";", StringSplitOptions.RemoveEmptyEntries);

                if (tokens[0] == "Add filter")
                {
                    filters.Add(tokens[1] + " " + tokens[2]);
                }
                else if (tokens[0] == "Remove filter")
                {
                    filters.Remove(tokens[1] + " " + tokens[2]);
                }
            }

            foreach (var filter in filters)
            {
                var commands = filter.Split(" ");

                if (commands[0] == "Starts")
                {
                    names = names.Where(p => !p.StartsWith(commands[2])).ToList();
                }
                else if (commands[0] == "Ends")
                {
                    names = names.Where(p => !p.EndsWith(commands[2])).ToList();
                }
                else if (commands[0] == "Length")
                {
                    names = names.Where(p => p.Length != int.Parse(commands[1])).ToList();
                }
                else if (commands[0] == "Contains")
                {
                    names = names.Where(p => !p.Contains(commands[1])).ToList();
                }   
            }
            if (names.Count > 0)
            {
                Console.WriteLine(string.Join(" ", names));
            }
        }
    }
}
