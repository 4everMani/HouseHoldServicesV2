using Cart.API.Entities;
using Cart.API.GrpcServices;
using Cart.API.Mapper;
using Cart.API.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;


namespace Cart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        private readonly CatalogGrpcService _grpcService;

        private readonly ILogger<CartController> _logger;

        private readonly IPublishEndpoint _publishEndpoint;

        public CartController(ICartRepository cartRepository, CatalogGrpcService grpcService, ILogger<CartController> logger, IPublishEndpoint publishEndpoint)
        {
            _cartRepository = cartRepository;
            _grpcService = grpcService;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{userName}", Name = "GetCart")]
        [ProducesResponseType(typeof(ServiceCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ServiceCart>> GetCart(string userName)
        {
            var cart = await _cartRepository.GetCart(userName);
            return Ok(cart ?? new ServiceCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ServiceCart>> UpdateCart([FromBody] ServiceCartWrite cart)
        {
            return Ok(await _cartRepository.UpdateCart(cart));
        }

        [HttpDelete("{userName}", Name = "DeleteCart")]
        [ProducesResponseType(typeof(ServiceCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCart(string userName)
        {
            await _cartRepository.DeleteCart(userName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CartCheckout([FromBody] CartCheckout cartCheckout)
        {
            var cart = await _cartRepository.GetCart(cartCheckout.UserName);

            if (cart == null)
            {
                _logger.LogInformation("Cart is empty for username {0}", cartCheckout.UserName);
                return BadRequest();
            }

            cart.Items = _cartRepository.ZipCodeAvailability(cartCheckout.ZipCode, cart.Items);

            if (cart != null && cart.Items != null && cart.Items.Count > 0)
            {
                var checkoutEvent = CartCheckoutMapper.Map(cart, cartCheckout);
                await _publishEndpoint.Publish(checkoutEvent);
                await DeleteCart(checkoutEvent.UserName);
                return Accepted();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
