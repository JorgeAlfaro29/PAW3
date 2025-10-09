using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetDataAsync();
    }
    public class TaskService(IRestProvider restProvider, IConfiguration configuration) : IService<TaskDTO>, ITaskService
    {
        public async Task<IEnumerable<TaskDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Task");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<TaskDTO>>(response);
        }
    }

    
}
