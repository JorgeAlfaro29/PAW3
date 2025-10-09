using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDTO>> GetDataAsync();
    }
    public class UserRoleService(IRestProvider restProvider, IConfiguration configuration) : IService<UserRoleDTO>, IUserRoleService
    {
        public async Task<IEnumerable<UserRoleDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "userRole");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<UserRoleDTO>>(response);
        }
    }

    
}
