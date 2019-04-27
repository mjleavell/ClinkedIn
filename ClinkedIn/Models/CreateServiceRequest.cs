using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class CreateServiceRequest
    {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
    }
}
