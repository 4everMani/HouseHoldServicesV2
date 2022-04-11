using Cart.API.Entities;
using System.Threading.Tasks;

namespace Cart.API.Repositories
{
    public interface ICartRepository
    {
        Task<ServiceCart> GetCart(string userName);

        Task<ServiceCart> UpdateCart(ServiceCartWrite cart);

        Task DeleteCart(string userName);
    }
}
