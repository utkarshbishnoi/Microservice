using ProductDetailService.Models;

namespace ProductDetailService.Repository
{
    public class ProductDetailRepository
    {
        public static readonly List<ProductDetailModel> _products = new List<ProductDetailModel>
        {
        new ProductDetailModel { ProductId = 1 ,Price = 20, Size = "medium", Design = "regular" },
        new ProductDetailModel { ProductId = 2 ,Price = 10, Size = "small", Design = "urban" }
        };

        public List<ProductDetailModel> GetAllProducts() => _products;

        public ProductDetailModel AddProduct(ProductDetailModel product)
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
        public ProductDetailModel GetProduct(int productId)
        {
            return _products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
