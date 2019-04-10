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
            ReleaseDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Interests { get; set; }
        // Enemies
        // Serivices
        // Friends
    }
}
