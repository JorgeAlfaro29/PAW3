using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;

namespace PAW3.Mvc.ServiceLocator
{
    public interface INotificationServiceMvc
    {
        Task<NotificationDTO?> CreateAsync(NotificationDTO notification);
        Task<bool> UpdateAsync(int id, NotificationDTO notification);
        Task<bool> DeleteAsync(int id);
    }
    public class NotificationServiceMvc(IRestProvider restProvider) : INotificationServiceMvc
    {
        private readonly string _baseUrl = "https://localhost:7130/api/NotificationServiceLocator/notifications";

        public async Task<NotificationDTO?> CreateAsync(NotificationDTO notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            // Serializamos el objeto
            var body = JsonSerializer.Serialize(notification);

            // POST a la API real de categorías
            var response = await restProvider.PostAsync(_baseUrl, body);

            // Deserializamos la respuesta
            return await JsonProvider.DeserializeAsync<NotificationDTO>(response);
        }

        public async Task<bool> UpdateAsync(int id, NotificationDTO notification)
        {
            var url = $"{_baseUrl}/{id}";
            var body = JsonSerializer.Serialize(notification);
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
