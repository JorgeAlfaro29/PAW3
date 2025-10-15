using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDTO>> GetDataAsync();

        Task<NotificationDTO?> CreateAsync(NotificationDTO notification);

        Task<bool> UpdateAsync(NotificationDTO notification);
        Task<bool> DeleteAsync(int id);
    }
    public class NotificationService(IRestProvider restProvider, IConfiguration configuration) : IService<NotificationDTO>, INotificationService
    {
        public async Task<IEnumerable<NotificationDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Notification");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<NotificationDTO>>(response);
        }

        public async Task<NotificationDTO?> CreateAsync(NotificationDTO notification)
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Notification");

            // Serializamos el objeto a JSON
            var body = JsonSerializer.Serialize(notification);

            // Mandamos el POST al RestProvider
            var response = await restProvider.PostAsync(url, body);

            // Deserializamos la respuesta a CategoryDTO
            return await JsonProvider.DeserializeAsync<NotificationDTO>(response);
        }


        public async Task<bool> UpdateAsync(NotificationDTO notification)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "Notification");
            var fullUrl = $"{baseUrl}/{notification.Id}";

            var body = JsonSerializer.Serialize(notification);
            var response = await restProvider.PutAsync(fullUrl, string.Empty, body);

            // Si la respuesta no está vacía, asumimos que fue exitosa
            return !string.IsNullOrWhiteSpace(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "Notification");
            var fullUrl = $"{baseUrl}/{id}";

            var response = await restProvider.DeleteAsync(fullUrl, string.Empty);

            return !string.IsNullOrWhiteSpace(response);
        }
    }

    
}
