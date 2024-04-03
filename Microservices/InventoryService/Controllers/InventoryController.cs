using InventoryService.Models;
using InvertoryService.Models.Dto;
using InvertoryService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InvertoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryRepository _inventoryRepository;
        private readonly ProductDetailService _productDetailService;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(InventoryRepository inventoryRepository, ILogger<InventoryController> logger,ProductDetailService productDetailService)
        {
            _logger = logger;
            _inventoryRepository = inventoryRepository;
            _productDetailService = productDetailService;

        }

        [HttpGet("products")]
        public async Task<ActionResult> GetAllProducts()
        {
            try
            {
                var products = _inventoryRepository.GetAllProducts();
                var productDetails = await _productDetailService.GetAllProducts();
                List<ResponseDto> product = new List<ResponseDto>();
                foreach (var productItem in products)
                {
                    var temp =productItem;
                    var detail = productDetails.FirstOrDefault(pd => pd.ProductId == productItem.ProductId);
                    if (detail != null)
                    {
                        var productdto = new ResponseDto
                        {
                            ProductId = productItem.ProductId,
                            Name = productItem.Name,
                            Description = productItem.Description,
                            Quantity = productItem.Quantity,
                            Price = detail.Price,
                            Size = detail.Size,
                            Design = detail.Design
                        };
                        product.Add(productdto);
                    }
                }
                _logger.LogInformation("Retrieved all products.");

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet("product/{productId}")]
        public ActionResult<IEnumerable<Product>> GetProduct(int productId)
        {
            try
            {
                var product = _inventoryRepository.GetProduct(productId);
                _logger.LogInformation("Retrieved product.");
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("products")]
        public ActionResult<Product> AddProduct([FromBody] Product newProduct)
        {
            try
            {
                var addProduct = new ProductDto()
                {
                    Name = newProduct.Name,
                    Description = newProduct.Description,
                    Quantity=newProduct.Quantity,
                };
                var productDetail = new ProductDetailDto()
                {
                    Size = newProduct.Size,
                    Design = newProduct.Design,
                    Price = newProduct.Price,
                };
                _inventoryRepository.AddProduct(addProduct);
                _productDetailService.AddProducts(productDetail);
                _logger.LogInformation("Product Added.");
                return newProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }
        

        [Authorize(Roles = "Admin")]
        [HttpDelete("products/{productId}")]
        public ActionResult RemoveProduct(int productId)
        {
            try
            {
                _inventoryRepository.RemoveProduct(productId);
                _logger.LogInformation("Product removed successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing product.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }
    }
}
