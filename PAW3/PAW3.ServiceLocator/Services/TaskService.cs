using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetDataAsync();
        Task<TaskDTO?> CreateAsync(TaskDTO task);

        Task<bool> UpdateAsync(TaskDTO task);
        Task<bool> DeleteAsync(int id);
    }
    public class TaskService(IRestProvider restProvider, IConfiguration configuration) : IService<TaskDTO>, ITaskService
    {
        public async Task<IEnumerable<TaskDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Task");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<TaskDTO>>(response);
        }

        public async Task<TaskDTO?> CreateAsync(TaskDTO task)
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Task");

            // Serializamos el objeto a JSON
            var body = JsonSerializer.Serialize(task);

            // Mandamos el POST al RestProvider
            var response = await restProvider.PostAsync(url, body);

            // Deserializamos la respuesta a CategoryDTO
            return await JsonProvider.DeserializeAsync<TaskDTO>(response);
        }


        public async Task<bool> UpdateAsync(TaskDTO task)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "Task");
            var fullUrl = $"{baseUrl}/{task.Id}";

            var body = JsonSerializer.Serialize(task);
            var response = await restProvider.PutAsync(fullUrl, string.Empty, body);

            // Si la respuesta no está vacía, asumimos que fue exitosa
            return !string.IsNullOrWhiteSpace(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var baseUrl = configuration.GetStringFromAppSettings("APIS", "Task");
            var fullUrl = $"{baseUrl}/{id}";

            var response = await restProvider.DeleteAsync(fullUrl, string.Empty);

            return !string.IsNullOrWhiteSpace(response);
        }
    }

    
}
