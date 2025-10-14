using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDTO>> GetDataAsync();
        Task<InventoryDTO?> CreateAsync(InventoryDTO inventory);

        Task<bool> UpdateAsync(InventoryDTO inventory);
        Task<bool> DeleteAsync(int id);
    }
    public class InventoryService(IRestProvider restProvider, IConfiguration configuration) : IService<InventoryDTO>, IInventoryService
    {
        public async Task<IEnumerable<InventoryDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Inventory");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<InventoryDTO>>(response);
        }

        public async Task<InventoryDTO?> CreateAsync(InventoryDTO inventory)
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Inventory");

            // Serializamos el objeto a JSON
            var body = JsonSerializer.Serialize(inventory);

            // Mandamos el POST al RestProvider
            var response = await restProvider.PostAsync(url, body);

            // Deserializamos la respuesta a CategoryDTO
            return await JsonProvider.DeserializeAsync<InventoryDTO>(response);
        }


        public async Task<bool> UpdateAsync(InventoryDTO inventory)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "Inventory");
            var fullUrl = $"{baseUrl}/{inventory.InventoryId}";

            var body = JsonSerializer.Serialize(inventory);
            var response = await restProvider.PutAsync(fullUrl, string.Empty, body);

            // Si la respuesta no está vacía, asumimos que fue exitosa
            return !string.IsNullOrWhiteSpace(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "Inventory");
            var fullUrl = $"{baseUrl}/{id}";

            var response = await restProvider.DeleteAsync(fullUrl, string.Empty);

            return !string.IsNullOrWhiteSpace(response);
        }
    }

    
}
