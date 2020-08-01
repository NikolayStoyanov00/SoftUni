using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _5._Change_Town_Names_Casing
{
    class Program
    {
        static void Main(string[] args)
        {
            string countryName = Console.ReadLine();

            using (SqlConnection sqlConnection = new SqlConnection(
               @"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true"))
            {
                sqlConnection.Open();

                string queryToCheckIfCountryExists = "SELECT Id, Name FROM Countries\n" +
                                                     "WHERE Name = @countryName";

                SqlCommand checkIfCountryExistsCommand = new SqlCommand(queryToCheckIfCountryExists, sqlConnection);
                checkIfCountryExistsCommand.Parameters.AddWithValue("@countryName", countryName);

                int countryId = 0;
                string countryNameResult = "";

                using (SqlDataReader reader = checkIfCountryExistsCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            countryId = (int)reader[0];
                            countryNameResult = (string)reader[1];
                        }
                    }
                    else
                    {
                        Console.WriteLine("No town names were affected.");
                        return;
                    }
                }

                string queryToUpdateTownNames = "UPDATE Towns\n" +
                                                "SET Name = UPPER(Name)\n" +
                                                "WHERE CountryId = @countryId";

                SqlCommand updateTownNamesCommand = new SqlCommand(queryToUpdateTownNames, sqlConnection);
                updateTownNamesCommand.Parameters.AddWithValue("@countryId", countryId);
                int townsAffected = (int)updateTownNamesCommand.ExecuteNonQuery();

                if (townsAffected == 0)
                {
                    Console.WriteLine("No town names were affected.");
                    return;
                }

                Console.WriteLine($"{townsAffected} town names were affected.");

                string queryToGetTownNames = "SELECT Name FROM Towns\n" +
                                           "WHERE CountryId = @countryId";

                SqlCommand getTownNamesCommand = new SqlCommand(queryToGetTownNames, sqlConnection);
                getTownNamesCommand.Parameters.AddWithValue("@countryId", countryId);

                List<string> townNames = new List<string>();

                using (SqlDataReader reader = getTownNamesCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string townName = (string)reader[0];
                        townNames.Add(townName);
                    }
                }

                Console.Write('[');
                Console.Write(string.Join(", ", townNames));
                Console.WriteLine("]");
            }
        }
    }
}
