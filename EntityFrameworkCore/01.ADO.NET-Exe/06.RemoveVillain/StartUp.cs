namespace _06.RemoveVillain
{
    using Microsoft.Data.SqlClient;
    using System;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            // Create connection
            SqlConnection sqlConnection = new SqlConnection("Server=.;Integrated Security=true;Database=MinionsDB;");

            sqlConnection.Open();

            using (sqlConnection)
            {
                int input = int.Parse(Console.ReadLine());

                var villain = new SqlCommand("SELECT Name FROM Villains WHERE Id = @villainId", sqlConnection);
                villain.Parameters.AddWithValue("@villainId", input);
                var villainName = villain.ExecuteScalar();

                if (villainName == null)
                {
                    Console.WriteLine("No such villain was found.");
                }
                else
                {
                    var deleteMVCommand = new SqlCommand("DELETE FROM MinionsVillains WHERE VillainId = @villainId", sqlConnection);
                    deleteMVCommand.Parameters.AddWithValue("@villainId", input);
                    var affectedRows = deleteMVCommand.ExecuteNonQuery();

                    var deleteVillains = new SqlCommand("DELETE FROM Villains WHERE Id = @villainId", sqlConnection);
                    deleteVillains.Parameters.AddWithValue("@villainId", input);
                    deleteVillains.ExecuteNonQuery();

                    Console.WriteLine($"{villainName} was deleted.");
                    Console.WriteLine($"{affectedRows} minions were released.");
                }
            }
        }
    }
}
