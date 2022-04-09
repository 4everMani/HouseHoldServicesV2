using MongoDB.Driver;
using ServiceCatalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceCatalog.API.Data
{
    public interface IServiceCatalogContext
    {
        IMongoCollection<Service> Services { get; }
    }
}
