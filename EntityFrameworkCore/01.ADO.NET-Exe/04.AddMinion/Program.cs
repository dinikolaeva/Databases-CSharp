namespace _03.MinionNames
{
    using Microsoft.Data.SqlClient;
    using System;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection("Server=.;Integrated Security=true;Database=MinionsDB;");

            sqlConnection.Open();

            using (sqlConnection)
            {
                string[] minionsInput = Console.ReadLine()
                                        .Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string[] villainInput = Console.ReadLine()
                                        .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string minionName = minionsInput[1];
                int age = int.Parse(minionsInput[2]);
                string town = minionsInput[3];
                string villainName = villainInput[1];

                //search town of the minion
                using var towns = new SqlCommand("SELECT Id FROM Towns WHERE Name = @townName", sqlConnection);
                towns.Parameters.AddWithValue("@townName", town);
                var townId = towns.ExecuteScalar();

                if (townId == null)
                {
                    //insert town that not exist in database
                    using var insertTown = new SqlCommand("INSERT INTO Towns (Name) VALUES (@townName)", sqlConnection);
                    insertTown.Parameters.AddWithValue("@townName", town);
                    insertTown.ExecuteNonQuery();
                    Console.WriteLine($"Town {town} was added to the database.");
                }

                //search villain by name
                using var villain = new SqlCommand("SELECT Id FROM Villains WHERE Name = @Name", sqlConnection);
                var searchedVillain = villain.Parameters.AddWithValue("@Name", villainName);
                var villainId = villain.ExecuteScalar();

                if (villainId == null)
                {
                    //insert villain in database
                    using var insertVillain = new SqlCommand("INSERT INTO Villains (Name, EvilnessFactorId) VALUES (@villainName, 4)", sqlConnection);
                    insertVillain.Parameters.AddWithValue("@villainName", villainName);
                    insertVillain.ExecuteNonQuery();
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                villainId = villain.ExecuteScalar();
                }


                //add minion in the database
                using var minions = new SqlCommand("SELECT Id FROM Minions WHERE Name = @Name", sqlConnection);
                var searchedMinion = minions.Parameters.AddWithValue("@Name", minionName);
                var minionId = minions.ExecuteScalar();

                if (minionId == null)
                {
                    using var insertMinion = new SqlCommand(@"INSERT INTO Minions(Name, Age, TownId) VALUES(@nam, @age, @townId)", sqlConnection);
                    insertMinion.Parameters.AddWithValue("@nam", minionName);
                    insertMinion.Parameters.AddWithValue("@age", age);
                    insertMinion.Parameters.AddWithValue("@townId", townId);
                    insertMinion.ExecuteNonQuery();
                }

                //insert minion to be a servant of the villain
                using var insertConnection = new SqlCommand(@"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)", sqlConnection);
                insertConnection.Parameters.AddWithValue("@villainId", villainId);
                insertConnection.Parameters.AddWithValue("@minionId", minionId);
                insertConnection.ExecuteNonQuery();
                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }
        }
    }
}
