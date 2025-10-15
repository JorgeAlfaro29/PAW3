using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;

namespace PAW3.Mvc.ServiceLocator
{
    public interface IProductServiceMvc
    {
        Task<ProductDTO?> CreateAsync(ProductDTO product);
        Task<bool> UpdateAsync(int id, ProductDTO product);
        Task<bool> DeleteAsync(int id);
    }
    public class ProductServiceMvc(IRestProvider restProvider) : IProductServiceMvc
    {
        private readonly string _baseUrl = "https://localhost:7130/api/ProductServiceLocator/product";

        public async Task<ProductDTO?> CreateAsync(ProductDTO product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            // Serializamos el objeto
            var body = JsonSerializer.Serialize(product);

            // POST a la API real de categorías
            var response = await restProvider.PostAsync(_baseUrl, body);

            // Deserializamos la respuesta
            return await JsonProvider.DeserializeAsync<ProductDTO>(response);
        }

        public async Task<bool> UpdateAsync(int id, ProductDTO product)
        {
            var url = $"{_baseUrl}/{id}";
            var body = JsonSerializer.Serialize(product);
            var response = await restProvider.PutAsync(url, string.Empty, body);
            return !string.IsNullOrWhiteSpace(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var url = $"{_baseUrl}/{id}";
            var response = await restProvider.DeleteAsync(url, string.Empty);
            return !string.IsNullOrWhiteSpace(response);
        }
    }

    
}
