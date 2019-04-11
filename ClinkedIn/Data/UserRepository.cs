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
            //new User("WayneWorld","3425dsfsa", new DateTime(2020, 1, 31)){ Id = "094b3963-e404-49fa-923f-37dd3ce610b7" },
            new User("WayneWorld","3425dsfsa", new DateTime(2020, 1, 31)){ Id = "094b3963" },
            new User("OtherAdam","asjdfasd", new DateTime(2025, 5, 15)){ Id = "1ab01a37" },
            //new User("OtherAdam","asjdfasd", new DateTime(2025, 5, 15)){ Id = "1ab01a37-2718-4852-b6c0-65668e71c223" },
            new User("Chase","runFasterYouCantCatchMe", new DateTime(2023, 10, 31)){ Id = "3256cd61-872d-4c65-858a-e5b54a80c4c9" },
            new User("TedBundy","I@mTheW0rst", new DateTime(2134, 2, 14)){ Id = "f04da242-77bd-49ba-a13e-b186c05878ed" },
            new User("GoldenState","99dj$2!&adfg", new DateTime(2074, 6, 3)){ Id = "df7472d4-dc25-4ba4-8d03-8dfe4cf2481e" },
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

        public User GetSingleUser(string userId)
        {
            var singleUser = _users.FirstOrDefault(user => user.Id == userId);
            return singleUser;
        }

        public void DeleteUser(string userId)
        {
            var user = _users.FirstOrDefault(singleUser => singleUser.Id == userId);
            _users.Remove(user);
        }
    }
}
