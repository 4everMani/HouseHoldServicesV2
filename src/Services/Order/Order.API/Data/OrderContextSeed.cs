using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Data
{
    public static class OrderContextSeed
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<OrderContext>());
            }
        }
        private static void SeedData(OrderContext orderContext)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreConfiguredOrders());
                orderContext.Services.AddRange(GetPreConfiguredServices());
                orderContext.SaveChanges();
                Console.WriteLine("Seed database completed");
            }
        }

        private static IEnumerable<Order> GetPreConfiguredOrders()
        {

            return new List<Order>
            {
                new Order() {UserName = "mani07", TotalPrice = 2312, ZipCode = 841226, CreatedOn = DateTime.Parse("2022-04-17T20:44:46.5638556+05:30"), IsCancelled = false, OrderedBy = "Manish Jaiswal",
                    },
            };
        }

        private static IEnumerable<Service> GetPreConfiguredServices()
        {
            return new List<Service>
            {
               new Service(){ CreatedOn = DateTime.Parse("2022-04-17T20:44:46.5638556+05:30"), Price = 2312, ProviderContactNumber = 2039456372, ProviderEmail = "fit32@gmail.com", ServiceName = "Fitness trainer", ServiceProvider="A-Z Fitness" }
            };
        }
    }
}
