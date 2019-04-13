using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class Interests
    {
        public Interests(string username, string interest)
        {
            Username = username;
            Interest = interest;

        }

        public string Username { get; }
        public string Interest { get; set; }
    }
}
