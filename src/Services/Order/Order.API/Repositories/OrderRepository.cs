using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.API.Data;
using Ordering.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _dbContext;

        private readonly ILogger<OrderRepository> logger;

        public OrderRepository(OrderContext dbContext, ILogger<OrderRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Order> CancelOrder(int id)
        {
            var order =  await _dbContext.Orders.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }
            order.IsCancelled = true;
            await UpdateAsync(order);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllCancelledOrders()
        {
            return await _dbContext.Orders.Where(p => p.IsCancelled == true).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetCancelledOrders(string userName)
        {
            return await _dbContext.Orders.Where(p => p.UserName == userName && p.IsCancelled == true).ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _dbContext.Orders.Where(p => p.Id == id).FirstOrDefaultAsync();

            return await GetService(order);
        }

        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
        {
            return await _dbContext.Orders.Where(p => p.UserName == userName).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task CreateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            await _dbContext.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Order> GetService(Order order)
        {
            var services = await _dbContext.Services.Where(i => i.OrderId == order.Id).ToListAsync();
            order.OrderedServices = (services);
            return order;
        }

    }
}
