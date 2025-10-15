using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetDataAsync();
        Task<RoleDTO?> CreateAsync(RoleDTO role);

        Task<bool> UpdateAsync(RoleDTO role);
        Task<bool> DeleteAsync(int id);
    }
    public class RoleService(IRestProvider restProvider, IConfiguration configuration) : IService<RoleDTO>, IRoleService
    {
        public async Task<IEnumerable<RoleDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Role");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<RoleDTO>>(response);
        }

        public async Task<RoleDTO?> CreateAsync(RoleDTO role)
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Role");

            // Serializamos el objeto a JSON
            var body = JsonSerializer.Serialize(role);

            // Mandamos el POST al RestProvider
            var response = await restProvider.PostAsync(url, body);

            // Deserializamos la respuesta a CategoryDTO
            return await JsonProvider.DeserializeAsync<RoleDTO>(response);
        }


        public async Task<bool> UpdateAsync(RoleDTO role)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "Role");
            var fullUrl = $"{baseUrl}/{role.RoleId}";

            var body = JsonSerializer.Serialize(role);
            var response = await restProvider.PutAsync(fullUrl, string.Empty, body);

            // Si la respuesta no está vacía, asumimos que fue exitosa
            return !string.IsNullOrWhiteSpace(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "Role");
            var fullUrl = $"{baseUrl}/{id}";

            var response = await restProvider.DeleteAsync(fullUrl, string.Empty);

            return !string.IsNullOrWhiteSpace(response);
        }
    }

    
}
