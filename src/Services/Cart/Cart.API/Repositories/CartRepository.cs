using Cart.API.Entities;
using Cart.API.GrpcServices;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cart.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;

        private readonly CatalogGrpcService _grpcService;

        public CartRepository(IDistributedCache redisCache, CatalogGrpcService grpcService)
        {
            _redisCache = redisCache;
            _grpcService = grpcService;
        }


        public async Task<ServiceCart> GetCart(string userName)
        {

            var cart = await _redisCache.GetStringAsync(userName);

            if (string.IsNullOrEmpty(cart))
            {
                return null;
            }

            var deserializedCart =  JsonConvert.DeserializeObject<ServiceCartWrite>(cart);

            return await GetCartDetails(deserializedCart);
        }

        public async Task<ServiceCart> UpdateCart(ServiceCartWrite cart)
        {
            await _redisCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));

            return await GetCart(cart.UserName);
        }

        public async Task DeleteCart(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ServiceCart> GetCartDetails(ServiceCartWrite cartWrite)
        {
            var serviceCart = new ServiceCart();

            serviceCart.UserName = cartWrite.UserName;

            var total = 0;

            // TODO : Communicate with ServiceCatalog GRPC to get information about service.
            if (cartWrite.serviceIds != null && cartWrite.serviceIds.Count > 0)
            {
                foreach (var id in cartWrite.serviceIds)
                {
                    var item = new ServiceCartItem();
                    var pincodes = new List<decimal>();
                    var itemDetail = await _grpcService.GetCatalog(id);
                    item.ServiceId = id;
                    item.ServiceName = itemDetail.ServiceName;
                    item.ServiceProvider = itemDetail.ServiceProvider;
                    item.ProviderEmail = itemDetail.ProviderEmail;
                    item.Price = itemDetail.Price;
                    total += itemDetail.Price;
                    item.ProviderContactNumber = itemDetail.ProviderContactNumber;

                    foreach (var pin in itemDetail.PinCodeCovers)
                    {

                        pincodes.Add((decimal)pin);
                    }
                    item.PinCodeCovers = pincodes;
                    serviceCart.Items.Add(item);
                }
            }
            serviceCart.TotalPrice = total;

            return serviceCart;
        }

    }
}
