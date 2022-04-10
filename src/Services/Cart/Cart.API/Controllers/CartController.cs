using Cart.API.Entities;
using Cart.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Cart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
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
        public async Task<ActionResult<ServiceCart>> UpdateCart([FromBody] ServiceCart cart)
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
    }
}
