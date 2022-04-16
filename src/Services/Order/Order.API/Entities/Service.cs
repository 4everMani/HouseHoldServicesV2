using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Entities
{
    public class Service
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string ServiceName { get; set; }

        public string ServiceProvider { get; set; }

        public string ProviderEmail { get; set; }

        public decimal ProviderContactNumber { get; set; }

        public int OrderId { get; set; }
    }
}
