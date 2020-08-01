using System;

namespace _04
{
    public class Program
    {
        static void Main(string[] args)
        {
            ICallable phoneCallings = new Smartphone();
            IBrowsable phoneBrowsings = new Smartphone();

            string[] phoneNumbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string[] websites = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var phoneNumber in phoneNumbers)
            {
                Console.WriteLine(phoneCallings.Call(phoneNumber)); 
            }

            foreach (var website in websites)
            {
                Console.WriteLine(phoneBrowsings.Browse(website));
            }
        }
    }
}
