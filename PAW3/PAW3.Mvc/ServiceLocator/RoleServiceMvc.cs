using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;

namespace PAW3.Mvc.ServiceLocator
{
    public interface IRoleServiceMvc
    {
        Task<RoleDTO?> CreateAsync(RoleDTO role);
        Task<bool> UpdateAsync(int id, RoleDTO role);
        Task<bool> DeleteAsync(int id);
    }
    public class RoleServiceMvc(IRestProvider restProvider) : IRoleServiceMvc
    {
        private readonly string _baseUrl = "https://localhost:7130/api/RoleServiceLocator/role";

        public async Task<RoleDTO?> CreateAsync(RoleDTO role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            // Serializamos el objeto
            var body = JsonSerializer.Serialize(role);

            // POST a la API real de categorías
            var response = await restProvider.PostAsync(_baseUrl, body);

            // Deserializamos la respuesta
            return await JsonProvider.DeserializeAsync<RoleDTO>(response);
        }

        public async Task<bool> UpdateAsync(int id, RoleDTO role)
        {
            var url = $"{_baseUrl}/{id}";
            var body = JsonSerializer.Serialize(role);
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
