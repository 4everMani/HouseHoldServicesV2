using EventBus.Message.Events;
using MassTransit;
using Ordering.API.Mapper;
using Ordering.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    public class CartCheckoutConsumer : IConsumer<CartCheckoutEvent>
    {
        private readonly IOrderRepository _orderRepository;

        public CartCheckoutConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
        {
            var readOrder = CartCheckoutMapper.Map(context.Message);

            await _orderRepository.CreateOrder(readOrder);
        }
    }
}
