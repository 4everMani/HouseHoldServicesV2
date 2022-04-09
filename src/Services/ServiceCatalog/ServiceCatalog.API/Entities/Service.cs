using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceCatalog.API.Entities
{
    public class Service
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ServiceName { get; set; }

        public string ServiceProvider { get; set; }

        public decimal Price { get; set; }

        public string ProviderEmail { get; set; }

        public decimal ProviderContactNumber { get; set; }

        public List<decimal> PinCodeCovers { get; set; }

        public string Description { get; set; }

    }
}
