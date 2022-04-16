using Ordering.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();

        Task CreateOrder(Order order);

        Task<IEnumerable<Order>> GetOrderByUserName(string userName);

        Task<Order> GetOrderById(int id);

        Task<Order> CancelOrder(int id);

        Task<IEnumerable<Order>> GetCancelledOrders(string userName);

        Task<IEnumerable<Order>> GetAllCancelledOrders();


    }
}
