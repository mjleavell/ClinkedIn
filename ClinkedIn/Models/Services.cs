using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class Services
    {
        public Services(string username, string service)
        {
            Username = username;
            Service = service;
        }

        public string Username { get; }
        public string Service { get; set; }
    }
}
