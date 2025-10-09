using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface IUserActionService
    {
        Task<IEnumerable<UserActionDTO>> GetDataAsync();
    }
    public class UserActionService(IRestProvider restProvider, IConfiguration configuration) : IService<UserActionDTO>, IUserActionService
    {
        public async Task<IEnumerable<UserActionDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Action");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<UserActionDTO>>(response);
        }

    }

    
}
