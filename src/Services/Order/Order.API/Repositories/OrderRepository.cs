using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.API.Data;
using Ordering.API.Entities;
using Ordering.API.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _dbContext;

        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(OrderContext dbContext, ILogger<OrderRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ReadOrder> CancelOrder(int id)
        {
            var order =  await _dbContext.Orders.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }
            order.IsCancelled = true;
            await UpdateAsync(order);
            return await GetService(order);
        }

        public async Task<IEnumerable<ReadOrder>> GetAllCancelledOrders()
        {
            var orders = await _dbContext.Orders.Where(p => p.IsCancelled == true).ToListAsync();
            
            if (orders == null)
            {
                return null;
            }

            return await GetOrderList(orders);
        }

        public async Task<IEnumerable<ReadOrder>> GetCancelledOrders(string userName)
        {
            var orders = await _dbContext.Orders.Where(p => p.UserName == userName && p.IsCancelled == true).ToListAsync();
            return await GetOrderList(orders);
        }

        public async Task<ReadOrder> GetOrderById(int id)
        {
            var order = await _dbContext.Orders.Where(p => p.Id == id).FirstOrDefaultAsync();

            return await GetService(order);
        }

        public async Task<IEnumerable<ReadOrder>> GetOrderByUserName(string userName)
        {
            var orders = await _dbContext.Orders.Where(p => p.UserName == userName).ToListAsync();
            return await GetOrderList(orders);
        }

        public async Task<IEnumerable<ReadOrder>> GetOrders()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            return await GetOrderList(orders);
        }

        public async Task CreateOrder(ReadOrder readOrder)
        {
            if (readOrder == null)
            {
                throw new ArgumentNullException(nameof(readOrder));
            }
            var order = OrderMapper.Map(readOrder);
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.Services.AddRangeAsync(readOrder.OrderedServices);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Order Created Successfully, " +
                " Information required for service providers are as follows =>\n Customer Name: {0} \n Customer ZipCode: {1}", order.OrderedBy, order.ZipCode);
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        private async Task<ReadOrder> GetService(Order order)
        {
            if (order == null)
            {
                return null;
            }
            var readOrder = new ReadOrder();
            var services = await _dbContext.Services.Where(i => i.CreatedOn == order.CreatedOn).ToListAsync();
            readOrder.ZipCode = order.ZipCode;
            readOrder.Id = order.Id;
            readOrder.UserName = order.UserName;
            readOrder.TotalPrice = order.TotalPrice;
            readOrder.OrderedBy = order.OrderedBy;
            readOrder.IsCancelled = order.IsCancelled;
            readOrder.CreatedOn = order.CreatedOn;
            readOrder.OrderedServices = services;
            return readOrder;
        }

        private async Task<IEnumerable<ReadOrder>> GetOrderList(List<Order> orders)
        {
            var readOrderList = new List<ReadOrder>();

            if (orders != null && orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    readOrderList.Add(await GetService(order));
                }
            }
            return readOrderList;
        }

    }
}
