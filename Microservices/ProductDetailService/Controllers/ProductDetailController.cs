using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductDetailService.Models;
using ProductDetailService.Repository;

namespace ProductDetailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly ProductDetailRepository _productDetailRepository;

        private readonly ILogger<ProductDetailController> _logger;

        public ProductDetailController(ProductDetailRepository productDetailRepository, ILogger<ProductDetailController> logger)
        {
            _logger = logger;
            _productDetailRepository = productDetailRepository;
        }

        [HttpGet("productsdetails")]
        public ActionResult<IEnumerable<ProductDetailModel>> GetAllProducts()
        {
            try
            {
                var products = _productDetailRepository.GetAllProducts();
                _logger.LogInformation("Retrieved all products details.");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("productdetail/{productId}")]
        public ActionResult<IEnumerable<ProductDetailModel>> GetProduct(int productId)
        {
            try
            {
                var product = _productDetailRepository.GetProduct(productId);
                _logger.LogInformation("Retrieved product details.");
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
         [Authorize(Roles = "Admin")]
        [HttpPost("productdetail")]
        public ActionResult<ProductDetailModel> AddProduct([FromBody] ProductDetailModel newProduct)
        {
            try
            {
                var addedProduct = _productDetailRepository.AddProduct(newProduct);
                _logger.LogInformation("Product Deatils Added.");
                return CreatedAtAction(nameof(GetAllProducts), new { id = addedProduct.ProductId }, addedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("productdetail/{productId}")]
        public ActionResult RemoveProduct(int productId)
        {
            try
            {
                _productDetailRepository.RemoveProduct(productId);
                _logger.LogInformation("Product Detail removed successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing product.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }
    }
}
