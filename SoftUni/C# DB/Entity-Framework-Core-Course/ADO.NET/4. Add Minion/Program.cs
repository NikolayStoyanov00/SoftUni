using Microsoft.Data.SqlClient;
using System;
using System.Linq;

namespace _4._Add_Minion
{
    class Program
    {
        static void Main(string[] args)
        {
            string minionInput = Console.ReadLine();
            string[] minionData = minionInput
                .Split(':', StringSplitOptions.RemoveEmptyEntries)
                .ToArray()
                [1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string minionName = minionData[0];
            string minionAge = minionData[1];
            string minionTown = minionData[2];

            string villainInput = Console.ReadLine();
            string villainName = villainInput
                .Split(':')[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];

            using (SqlConnection sqlConnection = new SqlConnection(
               @"Server=.\SQLEXPRESS;Database=MinionsDB;Integrated Security=true"))
            {
                sqlConnection.Open();

                string queryToCheckIfTownExists =
                    "SELECT t.Name FROM Towns AS t\n" +
                    $"WHERE t.Name = '{minionTown}'";

                SqlCommand checkIfTownExistsCommand = new SqlCommand(queryToCheckIfTownExists, sqlConnection);
                string townName = (string)checkIfTownExistsCommand.ExecuteScalar();

                if (townName == null)
                {
                    string queryToAddTown =
                        "INSERT INTO Towns (Name, CountryId)\n" +
                        $"VALUES (@minionTown, 1)";

                    SqlCommand addTownCommand = new SqlCommand(queryToAddTown, sqlConnection);
                    addTownCommand.Parameters.AddWithValue("@minionTown", minionTown);
                    addTownCommand.ExecuteNonQuery();
                    Console.WriteLine($"Town {minionTown} was added to the database.");
                }

                string queryToTakeTownId =
                    "SELECT Id FROM Towns\n" +
                    $"WHERE Name = @minionTown";

                SqlCommand takeTownIdCommand = new SqlCommand(queryToTakeTownId, sqlConnection);
                takeTownIdCommand.Parameters.AddWithValue("@minionTown", minionTown);
                int townId = (int)takeTownIdCommand.ExecuteScalar();


                string queryToAddMinion = "INSERT INTO Minions (Name, Age, TownId)\n" +
                    $"VALUES (@minionName, @minionAge, @townId)";

                SqlCommand addMinionCommand = new SqlCommand(queryToAddMinion, sqlConnection);
                addMinionCommand.Parameters.AddWithValue("@minionName", minionName);
                addMinionCommand.Parameters.AddWithValue("@minionAge", minionAge);
                addMinionCommand.Parameters.AddWithValue("@townId", townId);

                addMinionCommand.ExecuteNonQuery();

                string queryToCheckIfVillainExists =
                    "SELECT Name FROM Villains\n" +
                    $"WHERE Name = @villainName";

                SqlCommand checkIfVillainExistsCommand = new SqlCommand(queryToCheckIfVillainExists, sqlConnection);
                checkIfVillainExistsCommand.Parameters.AddWithValue("@villainName", villainName);
                string resultVillain = (string)checkIfVillainExistsCommand.ExecuteScalar();

                if (resultVillain == null)
                {
                    string queryToAddVillain = "INSERT INTO Villains (Name, EvilnessFactorId)\n" +
                                              $"VALUES (@villainName, 4)";

                    SqlCommand addVillainCommand = new SqlCommand(queryToAddVillain, sqlConnection);
                    addVillainCommand.Parameters.AddWithValue("@villainName", villainName);
                    addVillainCommand.ExecuteNonQuery();
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }

                string queryToTakeMinionId = "SELECT Id FROM Minions\n" +
                                             $"WHERE Name = @minionName";

                SqlCommand takeMinionIdCommand = new SqlCommand(queryToTakeMinionId, sqlConnection);
                takeMinionIdCommand.Parameters.AddWithValue("@minionName", minionName);
                int minionId = (int)takeMinionIdCommand.ExecuteScalar();

                string queryToTakeVillainId = "SELECT Id FROM Villains\n" +
                                             $"WHERE Name = @villainName";

                SqlCommand takeVillainIdCommand = new SqlCommand(queryToTakeVillainId, sqlConnection);
                takeVillainIdCommand.Parameters.AddWithValue("@villainName", villainName);
                int villainId = (int)takeVillainIdCommand.ExecuteScalar();

                string queryToAddMinionToVillain = "INSERT INTO MinionsVillains (MinionId, VillainId)\n" +
                                                   $"VALUES (@minionId, @villainId)";

                SqlCommand addMinionToVillainCommand = new SqlCommand(queryToAddMinionToVillain, sqlConnection);
                addMinionToVillainCommand.Parameters.AddWithValue("@minionId", minionId);
                addMinionToVillainCommand.Parameters.AddWithValue("@villainId", villainId);
                addMinionToVillainCommand.ExecuteNonQuery();
                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }
        }
    }
}
