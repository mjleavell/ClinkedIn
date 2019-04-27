using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class Services
    {
        public Services(string username, string service, decimal price)
        {
            Id = Guid.NewGuid().ToString();
            Username = username;
            Service = service;
            Price = price;
        }

        public string Id { get; set; }
        public string Username { get; }
        public string Service { get; set; }
        public decimal Price { get; set; }
    }
}
