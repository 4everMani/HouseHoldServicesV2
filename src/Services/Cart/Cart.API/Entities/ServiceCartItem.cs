using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Entities
{
    public class ServiceCartItem
    {
        public decimal Price { get; set; }

        public string ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string ServiceProvider { get; set; }

        public string ProviderEmail { get; set; }

        public decimal ProviderContactNumber { get; set; }
    }
}
