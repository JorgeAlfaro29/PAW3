using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Helper;

public interface IServiceMapper
{
    Task<IService<T>> GetServiceAsync<T>(string name);
}

public class ServiceMapper : IServiceMapper
{
    private readonly IServiceProvider serviceProvider;

    public ServiceMapper(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public Task<IService<T>> GetServiceAsync<T>(string name)
    {
        var service = name.ToLower() switch
        {
            "product" => (IService<T>)serviceProvider.GetRequiredService<IService<ProductDTO>>(),

            "category" => (IService<T>)serviceProvider.GetRequiredService<IService<CategoryDTO>>(),

            "category" => (IService<T>)serviceProvider.GetRequiredService<IService<CategoryDTO>>(),
            "component" => (IService<T>)serviceProvider.GetRequiredService<IService<ComponentDTO>>(),
            "inventory" => (IService<T>)serviceProvider.GetRequiredService<IService<InventoryDTO>>(),
            "notifications" => (IService<T>)serviceProvider.GetRequiredService<IService<NotificationDTO>>(),
            "role" => (IService<T>)serviceProvider.GetRequiredService<IService<RoleDTO>>(),
            "supplier" => (IService<T>)serviceProvider.GetRequiredService<IService<SupplierDTO>>(),
            "task" => (IService<T>)serviceProvider.GetRequiredService<IService<TaskDTO>>(),
            "user" => (IService<T>)serviceProvider.GetRequiredService<IService<UserDTO>>(),
            "useraction" => (IService<T>)serviceProvider.GetRequiredService<IService<UserActionDTO>>(),
            "userrole" => (IService<T>)serviceProvider.GetRequiredService<IService<UserRoleDTO>>(),

            _ => throw new ArgumentException($"Service not found for '{name}'")
        };

        return Task.FromResult(service);
    }
}

