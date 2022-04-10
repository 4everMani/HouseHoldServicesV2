using Cart.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cart.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;

        public CartRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }


        public async Task<ServiceCart> GetCart(string userName)
        {
            var cart = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(cart))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ServiceCart>(cart);
        }

        public async Task<ServiceCart> UpdateCart(ServiceCart cart)
        {
            await _redisCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));

            return await GetCart(cart.UserName);
        }

        public async Task DeleteCart(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

    }
}
