
namespace _07.PrintAllMinionNames
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection("Server=.;Integrated Security=true;Database=MinionsDB;");

            sqlConnection.Open();

            using (sqlConnection)
            {
                var minionsCommand = new SqlCommand("SELECT Name FROM Minions", sqlConnection);

                using (var reader = minionsCommand.ExecuteReader())
                {
                    var minions = new List<string>();

                    while (reader.Read())
                    {
                        minions.Add((string)reader["Name"]);
                    }

                    int count = 0;

                    for (int i = 0; i < minions.Count / 2; i++)
                    {
                        Console.WriteLine(minions[0 + count]);
                        Console.WriteLine(minions[minions.Count - 1 - count]);
                        count++;
                    }

                    if (minions.Count % 2 != 0)
                    {
                        Console.WriteLine(minions[minions.Count / 2]);
                    }
                }
            }
        }
    }
}
