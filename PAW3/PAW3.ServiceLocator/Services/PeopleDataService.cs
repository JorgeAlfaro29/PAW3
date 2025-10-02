using System;
using Microsoft.Extensions.Configuration;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Data.DTOs;
using PAW3.Data.Models;


namespace PAW3.ServiceLocator.Services
{
    public class PeopleDataService : IPeopleDataService
    {
        private readonly IRestProvider _restProvider;
        private readonly IConfiguration _configuration;

        public PeopleDataService(IRestProvider restProvider, IConfiguration configuration)
        {
            _restProvider = restProvider;
            _configuration = configuration;

        }

        public async Task<IEnumerable<string>> GetAsEnumerableStringAsync()
        {
            var url = _configuration.GetStringFromAppSettings("APIS", "PeopleData");
            var response = await _restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<string>>(response);
        }

        public async Task<IEnumerable<T>> GetPeopleAsync<T>()
        {
            var url = _configuration.GetStringFromAppSettings("APIS", "PeopleData");
            var response = await _restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<T>>(response);
        }

    }
    public interface IPeopleDataService
    {
        Task<IEnumerable<string>> GetAsEnumerableStringAsync();
        Task<IEnumerable<T>> GetPeopleAsync<T>();
    }
}
