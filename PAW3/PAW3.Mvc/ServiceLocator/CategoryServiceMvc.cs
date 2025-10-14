using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services;

namespace PAW3.Mvc.ServiceLocator
{
    public interface ICategoryServiceMvc
    {
        Task<CategoryDTO?> CreateAsync(CategoryDTO category);
        Task<bool> UpdateAsync(int id, CategoryDTO category);
        Task<bool> DeleteAsync(int id);
    }
    public class CategoryServiceMvc(IRestProvider restProvider) : ICategoryServiceMvc
    {
        private readonly string _baseUrl = "https://localhost:7130/api/CategoryServiceLocator/category";

        public async Task<CategoryDTO?> CreateAsync(CategoryDTO category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            // Serializamos el objeto
            var body = JsonSerializer.Serialize(category);

            // POST a la API real de categorías
            var response = await restProvider.PostAsync(_baseUrl, body);

            // Deserializamos la respuesta
            return await JsonProvider.DeserializeAsync<CategoryDTO>(response);
        }

        public async Task<bool> UpdateAsync(int id, CategoryDTO category)
        {
            var url = $"{_baseUrl}/{id}";
            var body = JsonSerializer.Serialize(category);
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
