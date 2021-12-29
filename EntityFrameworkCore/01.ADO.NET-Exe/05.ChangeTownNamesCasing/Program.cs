namespace _05.ChangeTownNamesCasing
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            // Create connection
            SqlConnection sqlConnection = new SqlConnection("Server=.;Integrated Security=true;Database=MinionsDB;");

            sqlConnection.Open();

            using (sqlConnection)
            {
                try
                {
                    string nameOfCountry = Console.ReadLine();

                    using var towns = new SqlCommand("SELECT t.Name FROM Towns as t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.Name = @countryName", sqlConnection);
                    towns.Parameters.AddWithValue("@countryName", nameOfCountry);

                    using var countries = new SqlCommand("SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName", sqlConnection);
                    countries.Parameters.AddWithValue("@countryName", nameOfCountry);
                    var counryId = countries.ExecuteScalar();

                    using var updateTowns = new SqlCommand("UPDATE Towns SET Name = UPPER(Name) WHERE CountryCode = @countryId", sqlConnection);
                    updateTowns.Parameters.AddWithValue("@countryId", counryId);
                    updateTowns.ExecuteNonQuery();

                    List<object> upperCaseTowns = new List<object>();

                    using (var reader = towns.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                upperCaseTowns.Add(reader["Name"]);
                            }
                            Console.WriteLine($"{upperCaseTowns.Count} town names were affected.");
                            Console.WriteLine($"[{string.Join(", ", upperCaseTowns)}]");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("No town names were affected.");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}