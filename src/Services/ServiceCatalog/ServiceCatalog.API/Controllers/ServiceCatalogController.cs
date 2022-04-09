using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceCatalog.API.Entities;
using ServiceCatalog.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ServiceCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceCatalogController : ControllerBase
    {
        private readonly IServiceRepository _repository;

        private readonly ILogger<ServiceCatalogController> _logger;

        public ServiceCatalogController(IServiceRepository repository, ILogger<ServiceCatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Service>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            var services = await _repository.GetServices();
            return Ok(services);
        }
        
        [HttpGet("{id:length(24)}", Name = "GetService")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Service), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Service>> GetServiceById(string id)
        {
            var service = await _repository.GetServiceById(id);
            if (service != null)
            {
                return Ok(service);
            }
            _logger.LogError($"Service with id: {id}, not found");
            return NotFound();
            
        }

        [Route("[action]/{providerName}", Name = "GetServiceByProvider")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Service>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Service>>> GetServiceByProvider(string providerName)
        {
            var service = await _repository.GetServiceByProvider(providerName);
            return Ok(service);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Service), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Service>> CreateService([FromBody] Service service)
        {
            await _repository.CreateService(service);
            return CreatedAtRoute("GetService", new { id = service.Id }, service);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Service), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateService([FromBody] Service service)
        {
            return Ok(await _repository.UpdateService(service));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteService")]
        public async Task<IActionResult> DeleteServiceById(string id)
        {
            return Ok(await _repository.DeleteService(id));
        }
    }
}
