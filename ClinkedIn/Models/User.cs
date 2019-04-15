﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class User
    {
        public User(string username, string password, DateTime releaseDate)
        {
            Id = Guid.NewGuid().ToString();
            Username = username;
            Password = password;
            ReleaseDate = releaseDate;
            Friends = new List<User>();
            Enemies = new List<User>();
            IsWarden = false;
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<string> Interests { get; set; } = new List<string> { };
        public List<string> Services { get; set; } = new List<string> { };
        public List<User> Friends { get; set; }
        public List<User> Enemies { get; set; }
        public bool IsWarden { get; set; }
    };

}
