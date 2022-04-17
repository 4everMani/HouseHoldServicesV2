using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Entities
{
    public class CartCheckout
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public decimal ZipCode { get; set; }


    }
}
