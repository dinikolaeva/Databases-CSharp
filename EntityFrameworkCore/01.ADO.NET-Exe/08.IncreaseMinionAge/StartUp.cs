
namespace _08.IncreaseMinionAge
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
                var inputId = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < inputId.Length; i++)
                {
                    int id = int.Parse(inputId[i]);

                    using var updateCommand = new SqlCommand("UPDATE Minions SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1 WHERE Id = @Id", sqlConnection);
                    updateCommand.Parameters.AddWithValue("@Id", id);
                    updateCommand.ExecuteNonQuery();
                }

                using var selectMinionsCommand = new SqlCommand("SELECT Name, Age FROM Minions", sqlConnection);

                using (var reader = selectMinionsCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
                    }
                }
            }
        }
    }
}
