using Microsoft.Data.SqlClient;
using System;

namespace _3._Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please insert a villain Id: ");
            int villainId = int.Parse(Console.ReadLine());

            using (SqlConnection sqlConnection = new SqlConnection(
               @"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true"))
            {
                sqlConnection.Open();

                string queryFindVillain =
                    "SELECT [Name] FROM Villains\n"
                    + $"WHERE Id = {villainId}";

                SqlCommand findVillainCommand = new SqlCommand(queryFindVillain, sqlConnection);

                string villainName = (string)findVillainCommand.ExecuteScalar();

                if (villainName == null)
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                }
                else
                {
                    Console.WriteLine($"Villain: {villainName}");

                    string queryVillainMinions =
                        "SELECT\n"
                        + "m.Name, \n"
                        + "m.Age\n"
                        + "FROM Villains AS v\n"
                        + "JOIN MinionsVillains AS mv ON v.Id = mv.VillainId\n"
                        + "JOIN Minions AS m ON m.Id = mv.MinionId\n"
                        + $"WHERE v.Id = {villainId}\n" 
                        + "ORDER BY m.Name";

                    SqlCommand findVillainMinions = new SqlCommand(queryVillainMinions, sqlConnection);

                    int counter = 1;
                    using (SqlDataReader reader = findVillainMinions.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string minionName = (string)reader[0];
                                int minionAge = (int)reader[1];

                                Console.WriteLine($"{counter}. {minionName} {minionAge}");

                                counter++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("(no minions)");
                        }
                    }
                }
            }
        }
    }
}
