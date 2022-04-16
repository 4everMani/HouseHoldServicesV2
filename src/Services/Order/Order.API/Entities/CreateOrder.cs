using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Entities
{
    public class CreateOrder
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string CartId { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public decimal ZipCode { get; set; }
    }
}
