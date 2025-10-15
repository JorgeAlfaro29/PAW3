using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;

namespace PAW3.Mvc.ServiceLocator
{
    public interface ITaskServiceMvc
    {
        Task<TaskDTO?> CreateAsync(TaskDTO task);
        Task<bool> UpdateAsync(int id, TaskDTO task);
        Task<bool> DeleteAsync(int id);
    }
    public class TaskServiceMvc(IRestProvider restProvider) : ITaskServiceMvc
    {
        private readonly string _baseUrl = "https://localhost:7130/api/TaskServiceLocator/task";

        public async Task<TaskDTO?> CreateAsync(TaskDTO task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            // Serializamos el objeto
            var body = JsonSerializer.Serialize(task);

            // POST a la API real de categorías
            var response = await restProvider.PostAsync(_baseUrl, body);

            // Deserializamos la respuesta
            return await JsonProvider.DeserializeAsync<TaskDTO>(response);
        }

        public async Task<bool> UpdateAsync(int id, TaskDTO task)
        {
            var url = $"{_baseUrl}/{id}";
            var body = JsonSerializer.Serialize(task);
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
