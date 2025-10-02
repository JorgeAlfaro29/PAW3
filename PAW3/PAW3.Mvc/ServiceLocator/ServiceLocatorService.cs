using Microsoft.Extensions.Configuration;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Data.DTOs;

namespace PAW3.Mvc.ServiceLocator;

public interface IServiceLocatorService
{
    Task<IEnumerable<PersonDTO>> GetDataAsync(string id);
}

public class ServiceLocatorService(IRestProvider restProvider) : IServiceLocatorService
{
    private readonly IRestProvider _restProvider = restProvider;

    public async Task<IEnumerable<PersonDTO>> GetDataAsync(string id)
    {
        var response = await _restProvider.GetAsync("https://localhost:7130/api/ServiceLocator/", id);
        return await JsonProvider.DeserializeAsync<IEnumerable<PersonDTO>>(response);
    }



}
