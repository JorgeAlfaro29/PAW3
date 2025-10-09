using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services
{
    public interface ISupplierService
    {
    }
    public class SupplierService(IRestProvider restProvider, IConfiguration configuration) : IService<SupplierDTO>, ISupplierService
    {
        public async Task<IEnumerable<SupplierDTO>> GetDataAsync()
        {
            var url = configuration.GetStringFromAppSettings("APIS", "Supplier");
            var response = await restProvider.GetAsync(url, null);
            return await JsonProvider.DeserializeAsync<IEnumerable<SupplierDTO>>(response);
        }
    }

    
}
