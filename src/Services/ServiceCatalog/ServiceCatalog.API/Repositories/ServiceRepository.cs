using MongoDB.Driver;
using ServiceCatalog.API.Data;
using ServiceCatalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceCatalog.API.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IServiceCatalogContext _context;

        public ServiceRepository(IServiceCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetServices()
        {
            return await _context.Services.Find(p => true).ToListAsync();
        }

        public async Task<Service> GetServiceById(string id)
        {
            return await _context.Services.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Service>> GetServiceByName(string name)
        {
            FilterDefinition<Service> filter = Builders<Service>.Filter.Eq(p => p.ServiceName, name);

            return await _context.Services.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServiceByProvider(string providerName)
        {
            FilterDefinition<Service> filter = Builders<Service>.Filter.Eq(p => p.ServiceProvider, providerName);

            return await _context.Services.Find(filter).ToListAsync();
        }

        public async Task CreateService(Service service)
        {
            await _context.Services.InsertOneAsync(service);
        }

        public async Task<bool> UpdateService(Service service)
        {
            var updateResult = await _context.Services.ReplaceOneAsync(filter: g => g.Id == service.Id, replacement: service);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteService(string id)
        {
            FilterDefinition<Service> filter = Builders<Service>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context.Services.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
