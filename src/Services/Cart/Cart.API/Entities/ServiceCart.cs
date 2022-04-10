using System.Collections.Generic;

namespace Cart.API.Entities
{
    public class ServiceCart
    {
        public string UserName { get; set; }

        public List<ServiceCartItem> Items { get; set; } = new List<ServiceCartItem>();

        public ServiceCart()
        {

        }

        public ServiceCart(string username)
        {
            UserName = username;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price;
                }
                return totalPrice;
            }
        }
    }
}
