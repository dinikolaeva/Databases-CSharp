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
                int input = int.Parse(Console.ReadLine());

                using var cmd = new SqlCommand("SELECT Name FROM Villains WHERE Id = @Id", sqlConnection);
                var searchedId = cmd.Parameters.AddWithValue("@Id", input);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Villain: {reader["Name"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No villain with ID {input} exists in the database.");
                        ;
                    }
                }

                using var selectFromMinionsVillains = new SqlCommand("SELECT ROW_NUMBER() " +
                    "OVER (ORDER BY m.Name) as RowNum, " +
                    "m.Name, " +
                    "m.Age " +
                    "FROM MinionsVillains AS mv " +
                    "JOIN Minions As m ON mv.MinionId = m.Id " +
                    "WHERE mv.VillainId = @Id " +
                    "ORDER BY m.Name", sqlConnection);
                var id = selectFromMinionsVillains.Parameters.AddWithValue("@Id", input);

                using (var reader = selectFromMinionsVillains.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["RowNum"]}. {reader["Name"]} {reader["Age"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"(no minions)");
                    }
                }
            }
        }
    }
}
