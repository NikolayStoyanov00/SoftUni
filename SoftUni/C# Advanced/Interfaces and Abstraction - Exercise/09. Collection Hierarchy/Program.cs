using System;

namespace _09._Collection_Hierarchy
{
    class Program
    {
        static void Main(string[] args)
        {
            AddCollection<string> addCollection = new AddCollection<string>();
            AddRemoveCollection<string> addRemoveCollection = new AddRemoveCollection<string>();
            MyList<string> myList = new MyList<string>();

            string[] input = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string[] addCollectionIndexes = new string[input.Length];
            string[] addRemoveCollectionIndexes = new string[input.Length];
            string[] myListIndexes = new string[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(addCollection.Add(input[i]) + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(addRemoveCollection.Add(input[i]) + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(myList.Add(input[i]) + " ");
            }
            Console.WriteLine();

            int removeOperations = int.Parse(Console.ReadLine());

            for (int i = 0; i < removeOperations; i++)
            {
                Console.Write(addRemoveCollection.Remove() + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < removeOperations; i++)
            {
                Console.Write(myList.Remove() + " ");
            }
            Console.WriteLine();
        }
    }
}
