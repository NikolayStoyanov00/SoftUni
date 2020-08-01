using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace _8._Increase_Minion_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> minionIds = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToList();

            List<string> minions = new List<string>();
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            using (SqlConnection sqlConnection = new SqlConnection(
               @"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true"))
            {
                sqlConnection.Open();

                string selectMinionsQuery = "SELECT Id, Name, Age\n" +
                                            "FROM Minions\n";

                SqlCommand selectMinionsCommand = new SqlCommand(selectMinionsQuery, sqlConnection);
                using (SqlDataReader reader = selectMinionsCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            int minionId = (int)reader[0];
                            string minionName = (string)reader[1];
                            int minionAge = (int)reader[2];

                            if (minionIds.Contains(minionId))
                            {
                                minionName = myTI.ToTitleCase(minionName);
                                minionAge += 1;
                            }

                            minions.Add($"{minionId} {minionName} {minionAge}");
                        }
                    }
                }

                string updateMinionQuery = "UPDATE Minions\n" +
                                           "SET Name = @minionName, Age = @minionAge\n" +
                                           "WHERE Id = @minionId";

                SqlCommand updateMinionCommand = new SqlCommand(updateMinionQuery, sqlConnection);

                foreach (var minion in minions)
                {
                    int minionId = int.Parse(minion.Split(' ')[0]);
                    string minionName = minion.Split(' ')[1];
                    int minionAge = int.Parse(minion.Split(' ')[2]);

                    updateMinionCommand.Parameters.AddWithValue("@minionName", minionName);
                    updateMinionCommand.Parameters.AddWithValue("@minionId", minionId);
                    updateMinionCommand.Parameters.AddWithValue("@minionAge", minionAge);
                    updateMinionCommand.ExecuteNonQuery();

                    updateMinionCommand = new SqlCommand(updateMinionQuery, sqlConnection);
                }

                Console.WriteLine(string.Join(Environment.NewLine, minions));
            }
        }
    }
}
