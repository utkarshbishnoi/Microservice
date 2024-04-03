using InventoryService.Models;
using InvertoryService.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InvertoryService.Repository
{
    public class ProductDetailService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductDetailService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<IEnumerable<ProductDetailDto>> GetAllProducts()
        {
            var client = _httpClientFactory.CreateClient("ProductDetail");
            var response = await client.GetAsync($"/api/ProductDetail/productsdetails");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject(apiContent);
            if (resp != null)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDetailDto>>(Convert.ToString(resp));
            }
            return new List<ProductDetailDto>();
        }
        public async Task<IEnumerable<ProductDetailDto>> AddProducts(ProductDetailDto newProduct)
        {
            var client = _httpClientFactory.CreateClient("ProductDetail");
            var response = await client.PostAsJsonAsync("/api/ProductDetail/productdetail", newProduct);
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject(apiContent);
            if (resp != null)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDetailDto>>(Convert.ToString(resp));
            }
            return new List<ProductDetailDto>();
        }
    }
}
