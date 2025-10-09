using System.Configuration;
using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Data.Models;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Helper;

namespace PAW3.Mvc.ServiceLocator;

public interface IServiceLocatorService
{
    Task<IEnumerable<T>> GetDataAsync<T>(string name);
    Task<T?> CreateAsync<T>(string name, T entity) where T : class;


}

public class ServiceLocatorService(IRestProvider restProvider, IServiceMapper serviceMapper) : IServiceLocatorService
{
    public async Task<IEnumerable<T>> GetDataAsync<T>(string name)
    {
        var response = await restProvider.GetAsync("https://localhost:7130/api/ServiceLocator/", name);
        return await JsonProvider.DeserializeAsync<IEnumerable<T>>(response);
    }

    public async Task<T?> CreateAsync<T>(string name, T entity) where T : class
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        var url = $"https://localhost:7130/api/ServiceLocator/{name}";

        var body = JsonSerializer.Serialize(entity);

        var response = await restProvider.PostAsync(url, body);

        return await JsonProvider.DeserializeAsync<T>(response);
    }



}
