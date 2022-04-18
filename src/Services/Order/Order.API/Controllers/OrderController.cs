using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ordering.API.Entities;
using Ordering.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        private readonly IOrderRepository _repo;

        public OrderController(ILogger<OrderController> logger, IOrderRepository repo)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        [Route("[action]", Name = "GetOrders")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _repo.GetOrders();
            return Ok(orders);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _repo.GetOrderById(id);

            if (order != null)
            {
                return Ok(order);
            }
            _logger.LogError($"Order with id: {id}, not found");
            return NotFound();
        }

        [Route("[action]/{userName}", Name = "GetOrderByUserName")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserName(string userName)
        {
            var orders = await _repo.GetOrderByUserName(userName);
            return Ok(orders);
        }

        //[HttpPost]
        //[ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<Order>> CreateOrder([FromBody] ReadOrder order)
        //{
        //    await _repo.CreateOrder(order);
        //    return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
        //}

        [Route("[action]/{id}", Name = "CancelOrder")]
        [HttpGet]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> CancelOrder(int id)
        {
            var cancelledOrder = await _repo.CancelOrder(id);

            return Ok(cancelledOrder);
        }

        [Route("[action]", Name = "GetCancelledOrders")]
        [HttpGet]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Order>>> GetCancelledOrders()
        {
            var cancelledOrders = await _repo.GetAllCancelledOrders();
            return Ok(cancelledOrders);
        }

        [Route("[action]/{userName}", Name = "GetCancelledOrderByUserName")]
        [HttpGet]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Order>>> GetCancelledOrderByUserName(string userName)
        {
            var cancelledOrders = await _repo.GetCancelledOrders(userName);
            return Ok(cancelledOrders);
        }
    }
}
