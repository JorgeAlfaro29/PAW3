using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetDataAsync();
        Task<CategoryDTO?> CreateAsync(CategoryDTO category);

        Task<bool> UpdateAsync(CategoryDTO category);
        Task<bool> DeleteAsync(int id);


    }

    public class CategoryService(IRestProvider restProvider, IConfiguration configuration) : IService<CategoryDTO>, ICategoryService
    {
        public async Task<IEnumerable<CategoryDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Category");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<CategoryDTO>>(response);
        }

        public async Task<CategoryDTO?> CreateAsync(CategoryDTO category)
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Category");

            // Serializamos el objeto a JSON
            var body = JsonSerializer.Serialize(category);

            // Mandamos el POST al RestProvider
            var response = await restProvider.PostAsync(url, body);

            // Deserializamos la respuesta a CategoryDTO
            return await JsonProvider.DeserializeAsync<CategoryDTO>(response);
        }


        public async Task<bool> UpdateAsync(CategoryDTO category)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "Category");
            var fullUrl = $"{baseUrl}/{category.CategoryId}";

            var body = JsonSerializer.Serialize(category);
            var response = await restProvider.PutAsync(fullUrl, string.Empty, body);

            // Si la respuesta no está vacía, asumimos que fue exitosa
            return !string.IsNullOrWhiteSpace(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "Category");
            var fullUrl = $"{baseUrl}/{id}";

            var response = await restProvider.DeleteAsync(fullUrl, string.Empty);

            return !string.IsNullOrWhiteSpace(response);
        }


        /*
        public async Task<CategoryDTO?> UpdateAsync(CategoryDTO category)
        {
            var url = $"{configuration.GetStringFromAppSettings("APIS", "Category")}/{category.CategoryId}";
            var body = await JsonProvider.SerializeAsync(category);
            var response = await restProvider.PutAsync(url, body, null);
            return await JsonProvider.DeserializeAsync<CategoryDTO>(response);
        }*/

    }

    }

