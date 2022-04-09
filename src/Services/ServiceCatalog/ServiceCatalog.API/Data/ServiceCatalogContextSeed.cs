using MongoDB.Driver;
using ServiceCatalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceCatalog.API.Data
{
    public class ServiceCatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Service> serviceCollection)
        {
            // checking the database for existing service
            bool existService = serviceCollection.Find(p => true).Any();

            if (!existService)
            {
                serviceCollection.InsertManyAsync(GetPreconfiguredServices());
            }


        }

        private static IEnumerable<Service> GetPreconfiguredServices()
        {
            return new List<Service>()
            {
                new Service()
                    {
                        Id = "602d2149e773f2a3990b47f5",
                        ServiceName = "Yoga",
                        ServiceProvider = "A-Z Fitness",
                        ProviderEmail = "yogaworld@gmail.com",
                        ProviderContactNumber = 1122334455,
                        PinCodeCovers = new List<decimal> { 226008, 226001, 226002},
                        Price = 3456,
                        Description = "From booking a yoga trainer at home to booking a class at a professional yoga studio, the Yoga World partners give customers an experience that makes this the most popular category on the app. For those who want to get fit mentally and physically, this is the go-to category."
                    },
                    new Service()
                    {
                        Id = "602d2149e773f2a3990b47f2",
                        ServiceName = "Fitness trainer",
                        ServiceProvider = "A-Z Fitness",
                        ProviderEmail = "fit32@gmail.com",
                        ProviderContactNumber = 2039456372,
                        PinCodeCovers = new List<decimal> { 226003, 226004, 226005 },
                        Price = 2312,
                        Description = "The fitness industry in India is worth a staggering Rs. 4,500 crore. It is not a surprise that a fitness trainer at home as a service makes it to this list. Book a fitness trainer and follow the pursuit of staying fit and leading a healthy lifestyle."
                    },
                    new Service()
                    {
                        Id = "602d2149e773f2a3990b47f3",
                        ServiceName = "Party make-up",
                        ServiceProvider = "Unisex Salon",
                        ProviderEmail = "salon_uni@gmail.com",
                        ProviderContactNumber = 3452348967,
                        PinCodeCovers = new List<decimal> { 226006, 226007, 226009 },
                        Price = 6699,
                        Description = "Beauty treatments aren’t just the best way to pamper yourself but also help you look your presentable best. With a host of services like facials, clean ups, waxing, threading, pedicure, manicure and the newly launched massage at home service."
                    },
                    new Service()
                    {
                        Id = "602d2149e773f2a3990b47f4",
                        ServiceName = "Party make-up",
                        ServiceProvider = "MakupOp",
                        ProviderEmail = "makeop07@gmail.com",
                        ProviderContactNumber = 9985673458,
                        PinCodeCovers = new List<decimal> { 226010, 226011, 226012 },
                        Price = 5643,
                        Description = "After the basic care, comes going out in style. Party make-up professionals on Urban Company use quality brands (MAC, Bobby Brown, Maybeline etc.) that do not put a dent on your pocket."
                    },
                    new Service()
                    {
                        Id = "602d2149e773f2a3990b47f6",
                        ServiceName = "Interior Designer",
                        ServiceProvider = "Home Interior Designer",
                        ProviderEmail = "interiordes09@gmail.com",
                        ProviderContactNumber = 8756447891,
                        PinCodeCovers = new List<decimal> { 226013, 226014, 226015 },
                        Price = 7645,
                        Description = "Home is always where the heart is. But care needs to be taken to have a comfortable abode that appeals to your every sense. If after a hard days work you come back to an inviting place, it ends your day on the perfect note every time."
                    }
            };
        }
    }
}
