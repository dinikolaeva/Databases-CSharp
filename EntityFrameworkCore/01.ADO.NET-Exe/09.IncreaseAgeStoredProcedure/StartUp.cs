namespace _09.IncreaseAgeStoredProcedure
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
                int id = int.Parse(Console.ReadLine());

                using var cmd = new SqlCommand("EXEC usp_GetOlder @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                using var selectCmd = new SqlCommand("SELECT Name, Age FROM Minions WHERE Id = @Id", sqlConnection);
                selectCmd.Parameters.AddWithValue("@Id", id);

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {                       
                        Console.WriteLine($"{reader["Name"]} – {reader["Age"]} years old");
                    }
                }
            }
        }
    }
}
