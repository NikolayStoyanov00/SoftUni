using System;

namespace HW05._Date_Modifier
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string startDate = Console.ReadLine();
            string endDate = Console.ReadLine();

            DateModifier dateModifier = new DateModifier();
            double difference = Math.Abs(dateModifier.CalculatesTheDifference(startDate, endDate));

            Console.WriteLine(difference);
        }
    }
}
