using Ordering.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Mapper
{
    public static class OrderMapper
    {
        public static Order Map(ReadOrder readOrder)
        {
            var order = new Order()
            {
                IsCancelled = readOrder.IsCancelled,
                CreatedOn = readOrder.CreatedOn,
                UserName = readOrder.UserName,
                OrderedBy = readOrder.OrderedBy,
                TotalPrice = readOrder.TotalPrice,
                ZipCode = readOrder.ZipCode,
            };

            return order;
        }
    }
}
