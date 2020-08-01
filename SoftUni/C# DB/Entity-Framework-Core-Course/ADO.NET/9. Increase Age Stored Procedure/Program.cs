using Microsoft.Data.SqlClient;
using System;

namespace _9._Increase_Age_Stored_Procedure
{
    class Program
    {
        static void Main(string[] args)
        {
            int minionId = int.Parse(Console.ReadLine());

            using (SqlConnection sqlConnection = new SqlConnection(
               @"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true"))
            {
                sqlConnection.Open();

                string queryToFindMinionWithId = "SELECT Name\n" +
                                                 "FROM Minions\n" +
                                                 "WHERE Id = @minionId";

                SqlCommand findMinionWithIdCommand = new SqlCommand(queryToFindMinionWithId, sqlConnection);
                findMinionWithIdCommand.Parameters.AddWithValue("@minionId", minionId);
                string minionName = (string)findMinionWithIdCommand.ExecuteScalar();

                string queryUpdateMinionAge = "UPDATE Minions\n" +
                                              "SET Age += 1\n" +
                                              "WHERE Id = @minionId";

                SqlCommand updateMinionAgeCommand = new SqlCommand(queryUpdateMinionAge, sqlConnection);
                updateMinionAgeCommand.Parameters.AddWithValue("@minionId", minionId);
                updateMinionAgeCommand.ExecuteNonQuery();


                string findUpdatedMinionQuery = "SELECT Name, Age\n" +
                                                "FROM Minions\n" +
                                                "WHERE Id = @minionId";

                SqlCommand findUpdatedMinionCommand = new SqlCommand(findUpdatedMinionQuery, sqlConnection);
                findUpdatedMinionCommand.Parameters.AddWithValue("@minionId", minionId);

                using (SqlDataReader reader = findUpdatedMinionCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = (string)reader[0];
                            int updatedAge = (int)reader[1];

                            Console.WriteLine($"{name} - {updatedAge} years old");
                        }
                    }
                }
            }
        }
    }
}
