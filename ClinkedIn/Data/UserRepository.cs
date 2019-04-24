﻿using ClinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Data
{
    public class UserRepository
    {
        const string ConnectionString = "Server = localhost; Database = ClinkedIn; Trusted_Connection = True;";

        static List<User> _users = new List<User>
        {
            new User("WayneWorld","3425dsfsa", new DateTime(2020, 1, 31)){ Id = "094b3963-e404-49fa-923f-37dd3ce610b7" },
            new User("OtherAdam","asjdfasd", new DateTime(2025, 5, 15)){ Id = "1ab01a37-2718-4852-b6c0-65668e71c223" },
            new User("Chase","runFasterYouCantCatchMe", new DateTime(2023, 10, 31)){ Id = "3256cd61-872d-4c65-858a-e5b54a80c4c9" },
            new User("TedBundy","I@mTheW0rst", new DateTime(2134, 2, 14)){ Id = "f04da242-77bd-49ba-a13e-b186c05878ed" },
            new User("JohnWayneGacy","99dj$2!&adfg", new DateTime(2074, 6, 3)){ Id = "df7472d4-dc25-4ba4-8d03-8dfe4cf2481e" },
            new User("JeffreyDahmer","2821349!&adfg", new DateTime(2238, 12, 24)){ Id = "a98a1255-2765-4530-b1cf-189b298d38a3" },
            new User("RichardRamirez","TheNightStalker98321", new DateTime(2190, 12, 12)){ Id = "4ebf96b1-591e-48da-933d-5c344f7a03ab" },
            new User("CharlesManson","aksdfhke1234", new DateTime(2138, 11, 19)){ Id = "334f467a-a2ae-4304-abcc-30d59923c192" },
            new User("HenryPope","Pris0nBre@kW@rden", new DateTime(2009, 1, 2)){ Id = "c77b3ad9-296e-4db7-b73f-e887aadbf57e", IsWarden = true },
        };

        static List<string> _intrest = new List<string> {
            "Killing",
            "Hair Braiding",
            "Murder",
            "Drinking",
            "Drugs"
        };

        public string ReadInterestList()
        {
            var listOfIntrest = " ";
            foreach (string intrestThing in _intrest)
            {
                listOfIntrest += intrestThing + (" , ");
            }
            return listOfIntrest;
        }

        public User AddUser(string username, string password, DateTime releaseDate)
        {
            var newUser = new User(username, password, releaseDate);
            _users.Add(newUser);
            return newUser;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>(); 
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand(); 
            getAllUsersCommand.CommandText = "select * from users";

            var reader = getAllUsersCommand.ExecuteReader(); // Excecute the reader! // if you don't care about the result and just want to know how many things were affected, use the ExecuteNonQuery
                                                             // ExecuteScalar for top left value - 1 column / 1 row
            while (reader.Read())
            {
                var id = reader["Id"].ToString(); //(int) is there to turn it into an int
                var username = reader["username"].ToString();
                var password = reader["password"].ToString();
                var releaseDate = (DateTime)reader["releaseDate"];
                var user = new User(username, password, releaseDate) { Id = id };

                users.Add(user);
            }

            connection.Close(); // Close it down!

            return users;
        }

        public User GetSingleUser(string userId)
        {
            var singleUser = _users.FirstOrDefault(user => user.Id == userId);
            return singleUser;
        }

        public bool DeleteUser(string userId)
        {
            var user = _users.FirstOrDefault(singleUser => singleUser.Id == userId);
            return _users.Remove(user);
        }
    }
}
