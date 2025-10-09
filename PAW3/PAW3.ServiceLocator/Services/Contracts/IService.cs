
namespace PAW3.ServiceLocator.Services.Contracts;

using PAW3.Data.Models;



public interface IService<T>
{
    Task<IEnumerable<T>> GetDataAsync();



}
