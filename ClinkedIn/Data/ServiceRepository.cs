using System;
using ClinkedIn.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Data
{
    public class ServiceRepository
    {
        const string ConnectionString = "Server = localhost; Database = ClinkedIn; Trusted_Connection = True;";

        public Services AddService(string name, string description, decimal price)
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = $@"Insert into Services (name,description,price)
                                            Output inserted.*
                                            Values(@name,@description,@price)";

                insertUserCommand.Parameters.AddWithValue("name", name);
                insertUserCommand.Parameters.AddWithValue("description", description);
                insertUserCommand.Parameters.AddWithValue("price", price);

                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var newService = new Services()
                    {
                        Id = (int)reader["id"],
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Price = (decimal)reader["price"],
                    };

                    return newService;
                }
            }

            throw new Exception("No Service found");
        }

        public List<Services> GetAllServices()
        {
            var services = new List<Services>();
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = "select * from services";

            var reader = getAllUsersCommand.ExecuteReader(); // Excecute the reader! // if you don't care about the result and just want to know how many things were affected, use the ExecuteNonQuery
                                                             // ExecuteScalar for top left value - 1 column / 1 row
            while (reader.Read())
            {
                var id = (int)reader["Id"]; //(int) is there to turn it into an int
                var name = reader["name"].ToString();
                var description = reader["description"].ToString();
                var price = (decimal)reader["price"];
                var newService = new Services(id, name, description, price);

                services.Add(newService);
            }

            connection.Close(); // Close it down!

            return services;
        }
    }
}
