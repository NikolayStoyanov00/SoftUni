using System;
using System.Collections.Generic;

namespace _06._Songs_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> songs = new Queue<string>(Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries));

            while (songs.Count > 0)
            {
                string line = Console.ReadLine();
                string[] commandParts = line.Split(" ");

                switch (commandParts[0])
                {
                    case "Play":
                        if (songs.Count > 0)
                        {
                            songs.Dequeue();
                        }
                        break;
                    case "Add":
                        string songName = line.Substring(4);

                        if (songs.Contains(songName))
                        {
                            Console.WriteLine($"{songName} is already contained!");
                        }
                        else
                        {
                            songs.Enqueue(songName);
                        }
                        break;
                    case "Show":
                        Console.WriteLine(string.Join(", ", songs));
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("No more songs!");
        }
    }
}
