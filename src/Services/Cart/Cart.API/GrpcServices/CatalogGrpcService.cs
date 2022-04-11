using ServiceCatalog.API.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.GrpcServices
{
    public class CatalogGrpcService
    {
        private readonly CatalogProtoService.CatalogProtoServiceClient _catalogProtoService;

        public CatalogGrpcService(CatalogProtoService.CatalogProtoServiceClient catalogProtoService)
        {
            _catalogProtoService = catalogProtoService ?? throw new ArgumentNullException(nameof(catalogProtoService));
        }

        public async Task<Catalog> GetCatalog(string id)
        {
            var catalogRequest = new GetCatalogRequest { ServiceId = id };

            return await _catalogProtoService.GetCatalogAsync(catalogRequest);
        }
    }
}
