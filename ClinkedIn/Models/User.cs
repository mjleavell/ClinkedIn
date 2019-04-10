using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class User
    {
        public User(string username, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            ReleaseDate = new DateTime(2100, 1, 31);
            Friends = new List<User>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime ReleaseDate { get; set; }
        //public List<Interest> Interests { get; set; }
        // Enemies
        // Serivices
        public List<User> Friends { get; set; }
    }
}
