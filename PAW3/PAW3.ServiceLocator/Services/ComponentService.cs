using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface IComponentService
    {
        Task<ComponentDTO?> CreateAsync(ComponentDTO component);
    }
    public class ComponentService(IRestProvider restProvider, IConfiguration configuration) : IService<ComponentDTO>, IComponentService
    {
        public async Task<IEnumerable<ComponentDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Component");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<ComponentDTO>>(response);
        }


        public async Task<ComponentDTO?> CreateAsync(ComponentDTO component)
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Component");

            // Serializamos el objeto a JSON
            var body = JsonSerializer.Serialize(component);

            // Mandamos el POST al RestProvider
            var response = await restProvider.PostAsync(url, body);

            // Deserializamos la respuesta a CategoryDTO
            return await JsonProvider.DeserializeAsync<ComponentDTO>(response);
        }


    }
}
