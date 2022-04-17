using Ordering.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<ReadOrder>> GetOrders();

        Task CreateOrder(ReadOrder order);

        Task<IEnumerable<ReadOrder>> GetOrderByUserName(string userName);

        Task<ReadOrder> GetOrderById(int id);

        Task<ReadOrder> CancelOrder(int id);

        Task<IEnumerable<ReadOrder>> GetCancelledOrders(string userName);

        Task<IEnumerable<ReadOrder>> GetAllCancelledOrders();


    }
}
