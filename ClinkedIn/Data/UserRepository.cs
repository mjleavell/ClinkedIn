using ClinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Data
{
    public class UserRepository
    {
        static List<User> _users = new List<User>
        {
            new User("WayneWorld","3425dsfsa", new DateTime(2020, 1, 31)),
            new User("OtherAdam","asjdfasd", new DateTime(2025, 5, 15)),
            new User("Chase","runFasterYouCantCatchMe", new DateTime(2023, 10, 31)),
            new User("TedBundy","I@mTheW0rst", new DateTime(2134, 2, 14)),
            new User("GoldenState","99dj$2!&adfg", new DateTime(2074, 6, 3)),
        };

        public User AddUser(string username, string password, DateTime releaseDate)
        {
            var newUser = new User(username, password, releaseDate);
            _users.Add(newUser);
            return newUser;
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User GetSingleUser(Guid userId)
        {
            var singleUser = _users.FirstOrDefault(user => user.Id == userId);
            return singleUser;
        }

        public void DeleteUser(Guid userId)
        {
            var user = _users.FirstOrDefault(singleUser => singleUser.Id == userId);
            _users.Remove(user);
        }
    }
}
