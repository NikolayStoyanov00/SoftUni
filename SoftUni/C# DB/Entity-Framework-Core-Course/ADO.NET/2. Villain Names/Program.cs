using Microsoft.Data.SqlClient;
using System;

namespace Introduction_to_DB_Apps
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection sqlConnection = new SqlConnection(
                @"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true"))
            {
                sqlConnection.Open();
                string query = "SELECT v.[Name], COUNT(mv.MinionId)\n"
                    + "FROM Villains AS v\n"
                    + "JOIN MinionsVillains AS mv ON mv.VillainId = v.Id\n"
                    + "GROUP BY v.[Name]\n"
                    + "HAVING COUNT(mv.MinionId) > 3\n"
                    + "ORDER BY COUNT(mv.MinionId) DESC";

                SqlCommand allVillainsNames = new SqlCommand(query, sqlConnection);

                SqlDataReader reader = allVillainsNames.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                }

                reader.Close();
            }
        }
    }
}
