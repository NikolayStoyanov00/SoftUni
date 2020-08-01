using System;
using System.Collections.Generic;
using System.Linq;

namespace __Average_Student_Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<double>> dictionary = new Dictionary<string, List<double>>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                double grade = double.Parse(input[1]);
                List<double> grades = new List<double>();
                grades.Add(grade);
                if (dictionary.ContainsKey(input[0]))
                {
                    dictionary[input[0]].Add(grade);
                }
                else
                {
                    dictionary.Add(input[0], grades);
                }
                grades = new List<double>();
            }

            foreach (var student in dictionary)
            {
                for (int i = 0; i < student.Value.Count; i++)
                {
                    Math.Round(student.Value[i], 2);
                }

                Console.Write($"{student.Key} -> ");

                for (int i = 0; i < student.Value.Count; i++)
                {
                    Console.Write($"{student.Value[i]:f2} ");
                }
                Console.WriteLine($"(avg: {student.Value.Average():F2})");
            }

        }
    }
}
