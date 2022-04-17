using EventBus.Message.Events;
using Ordering.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Mapper
{
    public static class CartCheckoutMapper
    {
       public static  ReadOrder Map(CartCheckoutEvent checkoutEvent)
       {
            var readOrder = new ReadOrder()
            {
                IsCancelled = checkoutEvent.IsCancelled,
                CreatedOn = checkoutEvent.CreatedOn,
                ZipCode = checkoutEvent.ZipCode,
                OrderedBy = checkoutEvent.OrderedBy,
                TotalPrice = checkoutEvent.TotalPrice,
                UserName = checkoutEvent.UserName,
                OrderedServices = MapService(checkoutEvent),
            };
            return readOrder;
       }

        public static List<Service> MapService(CartCheckoutEvent checkoutEvent)
        {
            var serviceList = new List<Service>();

            foreach(var service in checkoutEvent.OrderedServices)
            {
                var newService = new Service()
                {
                    Price = service.Price,
                    ProviderContactNumber = service.ProviderContactNumber,
                    ProviderEmail = service.ProviderEmail,
                    ServiceName = service.ServiceName,
                    ServiceProvider = service.ServiceProvider,
                    CreatedOn = checkoutEvent.CreatedOn,
                };
                serviceList.Add(newService);
            }
            return serviceList;
        }
    }
}
