using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Entities
{
    public class ServiceCartItem
    {
        [Required]
        public string ServiceId { get; set; }

        public decimal Price { get; set; }
        
        public string ServiceName { get; set; }

        public string ServiceProvider { get; set; }

        public string ProviderEmail { get; set; }

        public decimal ProviderContactNumber { get; set; }

        public List<decimal> PinCodeCovers { get; set; }
    }
}
