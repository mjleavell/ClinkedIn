using ClinkedIn.Models;
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

        //static List<User> _users;
        static List<User> _users = new List<User>
        {
            //new user("wayneworld","3425dsfsa", new datetime(2020, 1, 31)){ id = "094b3963-e404-49fa-923f-37dd3ce610b7" },
            //new user("otheradam","asjdfasd", new datetime(2025, 5, 15)){ id = "1ab01a37-2718-4852-b6c0-65668e71c223" },
            //new user("chase","runfasteryoucantcatchme", new datetime(2023, 10, 31)){ id = "3256cd61-872d-4c65-858a-e5b54a80c4c9" },
            //new user("tedbundy","i@mthew0rst", new datetime(2134, 2, 14)){ id = "f04da242-77bd-49ba-a13e-b186c05878ed" },
            //new user("johnwaynegacy","99dj$2!&adfg", new datetime(2074, 6, 3)){ id = "df7472d4-dc25-4ba4-8d03-8dfe4cf2481e" },
            //new user("jeffreydahmer","2821349!&adfg", new datetime(2238, 12, 24)){ id = "a98a1255-2765-4530-b1cf-189b298d38a3" },
            //new user("richardramirez","thenightstalker98321", new datetime(2190, 12, 12)){ id = "4ebf96b1-591e-48da-933d-5c344f7a03ab" },
            //new user("charlesmanson","aksdfhke1234", new datetime(2138, 11, 19)){ id = "334f467a-a2ae-4304-abcc-30d59923c192" },
            //new user("henrypope","pris0nbre@kw@rden", new datetime(2009, 1, 2)){ id = "c77b3ad9-296e-4db7-b73f-e887aadbf57e", iswarden = true },
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

        public User AddUser(string username, string password, DateTime releaseDate, int age, bool isPrisoner)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = $@"Insert into Users (username,password, releaseDate, age, isPrisoner)
                                            Output inserted.*
                                            Values(@username,@password,@releaseDate,@age,@isPrisoner)";

                insertUserCommand.Parameters.AddWithValue("username", username);
                insertUserCommand.Parameters.AddWithValue("password", password);
                insertUserCommand.Parameters.AddWithValue("releaseDate", releaseDate);
                insertUserCommand.Parameters.AddWithValue("age", age);
                insertUserCommand.Parameters.AddWithValue("isPrisoner", isPrisoner);

                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedPassword = reader["password"].ToString();
                    var insertedUsername = reader["username"].ToString();
                    var insertedReleaseDate = (DateTime)reader["releaseDate"];
                    var insertedAge = (int)reader["age"];
                    var insertedId = reader["Id"].ToString();
                    var insertedIsPrisoner = (bool)reader["isPrisoner"];

                    var newUser = new User(insertedUsername, insertedPassword, insertedReleaseDate, insertedAge, insertedIsPrisoner) { Id = insertedId };

                    return newUser;
                }
            }

            throw new Exception("No user found");
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>(); 
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand(); 
            getAllUsersCommand.CommandText = "select * from users";

            var reader = getAllUsersCommand.ExecuteReader(); 

            while (reader.Read())
            {
                var id = reader["Id"].ToString(); 
                var username = reader["username"].ToString();
                var password = reader["password"].ToString();
                var releaseDate = (DateTime)reader["releaseDate"];
                var age = (int)reader["age"];
                var isPrisoner = (bool)reader["isPrisoner"];
                var user = new User(username, password, releaseDate, age, isPrisoner) { Id = id };

                users.Add(user);
            }

            connection.Close();

            return users;
        }

        public User GetSingleUser(string userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var getSingleUserCommand = connection.CreateCommand();
                getSingleUserCommand.CommandText = @"SELECT *
                                                    FROM users u
                                                    WHERE u.id = @userId";

                getSingleUserCommand.Parameters.AddWithValue("@userId", userId);
                var reader = getSingleUserCommand.ExecuteReader();

                // if no users were returned, an error is thrown.
                if (!reader.Read())
                {
                    throw new InvalidOperationException("No user were returned.");
                }

                var singleUserId = reader["id"].ToString();
                var singleUserUsername = reader["username"].ToString();
                var singleUserPassword = reader["password"].ToString();
                var singleUserReleaseDate = (DateTime)reader["releaseDate"];
                var singleUserAge = (int)reader["age"];
                var singleUserIsPrisoner = (bool)reader["isPrisoner"];

                var singleUser = new User(singleUserUsername, singleUserPassword, singleUserReleaseDate, singleUserAge, singleUserIsPrisoner) { Id = singleUserId };
                return singleUser;
            }
            throw new Exception("No user found");
        }

        public bool UpdateIsPrisoner(string id)
        {
            var user = GetSingleUser(id);
            var isPrisoner= user.IsPrisoner;

            // if isPrisoner is true
            if (isPrisoner)
            {
                isPrisoner = false;
            }
            else
            {
                isPrisoner = true;
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var updateUserCommand = connection.CreateCommand();
                updateUserCommand.CommandText = $@"Update Users
                                                Set IsPrisoner = @isPrisoner
                                                where id = @id";

                updateUserCommand.Parameters.AddWithValue("@id", id);
                updateUserCommand.Parameters.AddWithValue("@isPrisoner", isPrisoner);
                var reader = updateUserCommand.ExecuteReader();
                return isPrisoner;
            }

            throw new Exception("No user found");
        }

        public User DeleteUser(int Id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var deleteUserCommand = connection.CreateCommand();
                deleteUserCommand.CommandText = $@"DELETE from Users
                                            Output deleted.*
                                            Where Id=@Id";

                deleteUserCommand.Parameters.AddWithValue("Id", Id);

                var reader = deleteUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var username = reader["username"].ToString();
                    var password = reader["password"].ToString();
                    var releaseDate = (DateTime)reader["releaseDate"];
                    var age = (int)reader["age"];
                    var id = reader["Id"].ToString();
                    var isPrisoner = (bool)reader["isPrisoner"];

                    var deletedUser = new User(username, password, releaseDate, age, isPrisoner) { Id = id };

                    return deletedUser;
                }
            }

            throw new Exception("No user found");
        }
    }
}
