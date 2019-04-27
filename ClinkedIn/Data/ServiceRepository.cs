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

        public Services AddService(string username, string service, decimal price)
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = $@"Insert into Services (username,service, price)
                                            Output inserted.*
                                            Values(@username,@service,@price)";

                insertUserCommand.Parameters.AddWithValue("username", username);
                insertUserCommand.Parameters.AddWithValue("service", service);
                insertUserCommand.Parameters.AddWithValue("price", price);

                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedUsername = reader["username"].ToString();
                    var insertedService = reader["service"].ToString();
                    var insertedPrice = (decimal)reader["price"];
                    var insertedId = reader["Id"].ToString();

                    var newService = new Services(insertedUsername, insertedService, insertedPrice) { Id = insertedId };

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
                var id = reader["Id"].ToString(); //(int) is there to turn it into an int
                var username = reader["username"].ToString();
                var service = reader["service"].ToString();
                var price = (decimal)reader["price"];
                var newService = new Services(username, service, price);

                services.Add(newService);
            }

            connection.Close(); // Close it down!

            return services;
        }
    }
}
