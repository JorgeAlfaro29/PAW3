using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Helper;

namespace PAW3.ServiceLocator.Controllers;

public class ServiceControllerBase : ControllerBase
{
    protected readonly Dictionary<string, Func<Task<IEnumerable<object>>>> ServiceResolvers;

    protected ServiceControllerBase(IServiceMapper serviceMapper)
    {
        ServiceResolvers = new()
        {
            ["product"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<ProductDTO>("product");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            },

            ["category"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<CategoryDTO>("category");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            },
            ["component"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<ComponentDTO>("component");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            },
            ["inventory"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<InventoryDTO>("inventory");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            },
            ["notifications"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<NotificationDTO>("notifications");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            },
            ["role"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<RoleDTO>("role");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            },
            ["supplier"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<SupplierDTO>("supplier");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            },
            ["task"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<TaskDTO>("task");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            },
            ["user"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<UserDTO>("user");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            },
            ["useraction"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<UserActionDTO>("useraction");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            },
            ["userrole"] = async () =>
            {
                // defer resolution until invocation time
                var service = await serviceMapper.GetServiceAsync<UserRoleDTO>("userrole");
                var data = await service.GetDataAsync();
                return data.Cast<object>();
            }




            // Example for another service:
            // ["category"] = async () =>
            // {
            //     var service = await serviceMapper.GetServiceAsync<CategoryDTO>("category");
            //     var data = await service.GetDataAsync();
            //     return data.Cast<object>();
            // }
        };


    }
}
