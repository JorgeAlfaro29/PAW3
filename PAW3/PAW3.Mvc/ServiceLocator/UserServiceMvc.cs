using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;

namespace PAW3.Mvc.ServiceLocator
{
    public interface IUserServiceMvc
    {
        Task<UserDTO?> CreateAsync(UserDTO user);
        Task<bool> UpdateAsync(int id, UserDTO user);
        Task<bool> DeleteAsync(int id);
    }
    public class UserServiceMvc(IRestProvider restProvider) : IUserServiceMvc
    {
        private readonly string _baseUrl = "https://localhost:7130/api/UserServiceLocator/user";

        public async Task<UserDTO?> CreateAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // Serializamos el objeto
            var body = JsonSerializer.Serialize(user);

            // POST a la API real de categorías
            var response = await restProvider.PostAsync(_baseUrl, body);

            // Deserializamos la respuesta
            return await JsonProvider.DeserializeAsync<UserDTO>(response);
        }

        public async Task<bool> UpdateAsync(int id, UserDTO user)
        {
            var url = $"{_baseUrl}/{id}";
            var body = JsonSerializer.Serialize(user);
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
