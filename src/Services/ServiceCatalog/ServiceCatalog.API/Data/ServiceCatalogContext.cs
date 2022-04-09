using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ServiceCatalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceCatalog.API.Data
{
    public class ServiceCatalogContext : IServiceCatalogContext
    {
        public ServiceCatalogContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(config.GetValue<string>("DatabaseSettings:DatabaseName"));

            Services = database.GetCollection<Service>("DatabaseSettings:CollectionName");

            ServiceCatalogContextSeed.SeedData(Services);
        }

        public IMongoCollection<Service> Services { get; }
    }
}
