using Cart.API.Entities;
using Cart.API.Utilities;
using EventBus.Message.Events;
using System;
using System.Collections.Generic;

namespace Cart.API.Mapper
{
    public static class CartCheckoutMapper
    {
        public static CartCheckoutEvent Map(ServiceCart serviceCart, CartCheckout checkout)
        {
            var model = new CartCheckoutEvent()
            {
                CreatedOn = DateTime.Now,
                OrderedBy = checkout.Name,
                ZipCode = checkout.ZipCode,
                TotalPrice = RepositoryHelper.GetTotal(serviceCart.Items),
                UserName = serviceCart.UserName,
                IsCancelled = false,
                OrderedServices = MapService(serviceCart.Items),
            };
            return model;
        }

        private static List<ServiceEvent> MapService(List<ServiceCartItem> items)
        {
            var events = new List<ServiceEvent>();

            foreach (var item in items)
            {
                var newService = new ServiceEvent()
                {
                    Price = item.Price,
                    ProviderContactNumber = item.ProviderContactNumber,
                    ProviderEmail = item.ProviderEmail,
                    ServiceName = item.ServiceName,
                    ServiceProvider = item.ServiceProvider,
                };
                events.Add(newService);
            }
            return events;
        }
    }
}
