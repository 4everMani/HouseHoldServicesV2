using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ordering.API.Entities
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string OrderedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsCancelled { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal ZipCode { get; set; }

        public List<Service> OrderedServices { get; set; }
    }
}
