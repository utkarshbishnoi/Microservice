using CartService.Models;
using CartService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartRepository _cartRepository;

        private readonly ILogger<CartController> _logger;

        public CartController(CartRepository cartRepository, ILogger<CartController> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }
        [Authorize]
        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<CartItem>> GetUserCart(int userId)
        {
            try
            {
                var userCart = _cartRepository.GetUserCart(userId);
                _logger.LogInformation("Retreived user cart");
                return Ok(userCart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user cart.");
                return StatusCode(500, "Internal Server Error");
            }

        }
        [Authorize]
        [HttpPost("{userId}")]
        public ActionResult AddToCart(int userId, [FromBody] CartItem cartItem)
        {
            try
            {
                _cartRepository.AddToCart(userId, cartItem);
                _logger.LogInformation("Add to cart successfully");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding to cart.");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [Authorize]
        [HttpPost("{userId}/checkout")]
        public ActionResult Checkout(int userId)
        {
            try
            {
                _cartRepository.Checkout(userId);
                _logger.LogInformation("checkout successfully");
                return Ok("Checkout successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking out.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
