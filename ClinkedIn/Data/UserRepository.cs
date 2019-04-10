using ClinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Data
{
    public class UserRepository
    {
        public List<User> _users = new List<User>
        {
            new User("WayneWorld","3425dsfsa"),
            new User("OtherAdam","asjdfasd"),
            new User("Chase","runFasterYouCantCatchMe"),
        };

        public User AddUser(string username, string password)
        {
            var newUser = new User(username, password);

            newUser.Id = _users.Count + 1;

            _users.Add(newUser);

            return newUser;
        }
    }
}
