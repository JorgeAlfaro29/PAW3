using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Data.Models;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetDataAsync();
        Task<UserDTO?> CreateAsync(UserDTO user);

        Task<bool> UpdateAsync(UserDTO user);
        Task<bool> DeleteAsync(int id);
    }
    public class UserService(IRestProvider restProvider, IConfiguration configuration) : IService<UserDTO>, IUserService
    {
        public async Task<IEnumerable<UserDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "User");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<UserDTO>>(response);
        }

        public async Task<UserDTO?> CreateAsync(UserDTO user)
        {
            var url = configuration.GetStringFromAppSettings("APIS", "User");

            // Serializamos el objeto a JSON
            var body = JsonSerializer.Serialize(user);

            // Mandamos el POST al RestProvider
            var response = await restProvider.PostAsync(url, body);

            // Deserializamos la respuesta a CategoryDTO
            return await JsonProvider.DeserializeAsync<UserDTO>(response);
        }


        public async Task<bool> UpdateAsync(UserDTO user)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "User");
            var fullUrl = $"{baseUrl}/{user.UserId}";

            var body = JsonSerializer.Serialize(user);
            var response = await restProvider.PutAsync(fullUrl, string.Empty, body);

            // Si la respuesta no está vacía, asumimos que fue exitosa
            return !string.IsNullOrWhiteSpace(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "User");
            var fullUrl = $"{baseUrl}/{id}";

            var response = await restProvider.DeleteAsync(fullUrl, string.Empty);

            return !string.IsNullOrWhiteSpace(response);
        }
    }

    
}
