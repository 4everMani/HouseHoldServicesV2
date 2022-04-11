using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cart.API.Entities
{
    public class ServiceCart
    {
        [Required]
        public string UserName { get; set; }

        public List<ServiceCartItem> Items { get; set; } = new List<ServiceCartItem>();

        public ServiceCart()
        {

        }

        public ServiceCart(string username)
        {
            UserName = username;
        }

        public decimal TotalPrice { get; set; }
    }
}
