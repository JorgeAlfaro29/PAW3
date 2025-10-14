using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;

namespace PAW3.Mvc.ServiceLocator
{
    public interface IInventoryServiceMvc
    {
        Task<InventoryDTO?> CreateAsync(InventoryDTO inventory);
        Task<bool> UpdateAsync(int id, InventoryDTO inventory);
        Task<bool> DeleteAsync(int id);
    }
    public class InventoryServiceMvc(IRestProvider restProvider) : IInventoryServiceMvc
    {
        private readonly string _baseUrl = "https://localhost:7130/api/InventoryServiceLocator/inventory";

        public async Task<InventoryDTO?> CreateAsync(InventoryDTO inventory)
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory));

            // Serializamos el objeto
            var body = JsonSerializer.Serialize(inventory);

            // POST a la API real de categorías
            var response = await restProvider.PostAsync(_baseUrl, body);

            // Deserializamos la respuesta
            return await JsonProvider.DeserializeAsync<InventoryDTO>(response);
        }

        public async Task<bool> UpdateAsync(int id, InventoryDTO inventory)
        {
            var url = $"{_baseUrl}/{id}";
            var body = JsonSerializer.Serialize(inventory);
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
