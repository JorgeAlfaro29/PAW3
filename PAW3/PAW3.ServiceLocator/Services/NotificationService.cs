using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface INotificationService
    {
    }
    public class NotificationService(IRestProvider restProvider, IConfiguration configuration) : IService<NotificationDTO>, INotificationService
    {
        public async Task<IEnumerable<NotificationDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Notification");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<NotificationDTO>>(response);
        }
    }

    
}
