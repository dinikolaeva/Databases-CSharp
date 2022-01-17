namespace _02.VillainNames
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
                using var cmd = new SqlCommand(
                              @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount 
                                FROM Villains AS v 
                                JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                                GROUP BY v.Id, v.Name 
                                HAVING COUNT(mv.VillainId) > 3 
                                ORDER BY COUNT(mv.VillainId)", sqlConnection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader["Name"];
                        var minionsCount = reader["MinionsCount"];

                        Console.WriteLine($"{name} - {minionsCount}");
                    }
                }
            }
        }
    }
}
