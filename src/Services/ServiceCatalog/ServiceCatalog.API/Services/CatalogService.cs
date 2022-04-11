using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ServiceCatalog.API.Protos;
using ServiceCatalog.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceCatalog.API.Services
{
    public class CatalogService : CatalogProtoService.CatalogProtoServiceBase
    {
        private readonly IServiceRepository _repo;

        private readonly ILogger<CatalogService> _logger;

        private IMapper _mapper;

        public CatalogService(IServiceRepository repo, ILogger<CatalogService> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<Catalog> GetCatalog(GetCatalogRequest request, ServerCallContext context)
        {
            var serviceCatalog = await _repo.GetServiceById(request.ServiceId);

            if (serviceCatalog == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Service with ServiceId {request.ServiceId} not found"));
            }
            _logger.LogInformation("Service information is retrived");
            var serviceModel = _mapper.Map<Catalog>(serviceCatalog);

            return serviceModel;
        }
    }
}
