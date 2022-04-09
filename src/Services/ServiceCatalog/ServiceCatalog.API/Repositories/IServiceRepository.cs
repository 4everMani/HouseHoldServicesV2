using ServiceCatalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceCatalog.API.Repositories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetServices();

        Task<Service> GetServiceById(string id);

        Task<IEnumerable<Service>> GetServiceByName(string name);

        Task<IEnumerable<Service>> GetServiceByProvider(string providerName);

        Task CreateService(Service service);

        Task<bool> UpdateService(Service service);

        Task<bool> DeleteService(string id);
    }
}
