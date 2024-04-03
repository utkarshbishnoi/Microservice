using InvertoryService.Models;
using InvertoryService.Models.Dto;

namespace InvertoryService.Repository
{
    public class InventoryRepository
    {
        public static readonly List<ProductDto> _products = new List<ProductDto>
            {
        new ProductDto { ProductId = 1 ,Quantity = 20, Name = "product1", Description = "desc1" },
        new ProductDto     { ProductId = 2 ,Quantity = 10, Name = "product2", Description = "desc2" }
        };

        public List<ProductDto> GetAllProducts()
        {
            return _products;
        }

        public ProductDto AddProduct(ProductDto product)
        {
            product.ProductId = _products.Count + 1;
            _products.Add(product);
            return product;
        }

        public void RemoveProduct(int productId)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
        public ProductDto GetProduct(int productId)
        {
            return _products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
