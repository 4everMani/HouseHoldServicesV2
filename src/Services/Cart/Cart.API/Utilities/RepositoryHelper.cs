using Cart.API.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Utilities
{
    public static class RepositoryHelper
    {
        public static decimal GetTotal(List<ServiceCartItem> items)
        {
            decimal total = 0;

            foreach(var item in items)
            {
                total += item.Price;
            }

            return total;
        }
    }
}
