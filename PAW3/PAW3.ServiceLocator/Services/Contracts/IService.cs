
namespace PAW3.ServiceLocator.Services.Contracts;

using PAW3.Data.Models;

namespace PAW3.ServiceLocator.Services.Contracts;


public interface IService<T>
{
    Task<IEnumerable<T>> GetDataAsync();



}
