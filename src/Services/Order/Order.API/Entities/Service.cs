using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Entities
{
    public class Service
    {
        public decimal Price { get; set; }

        public string ServiceName { get; set; }

        public string ServiceProvider { get; set; }

        public string ProviderEmail { get; set; }

        public decimal ProviderContactNumber { get; set; }
    }
}
