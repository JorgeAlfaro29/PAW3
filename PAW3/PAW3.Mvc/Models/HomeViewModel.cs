using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;

namespace PAW3.Mvc.Models;

public class HomeViewModel
{
    public string Title { get; set; } = "My App";
    public IEnumerable<string> Items { get; set; } = [];
    public object Dog { get; set; }
    public IEnumerable<ProductDTO> Products { get; set; } = [];
    public IEnumerable<CategoryDTO> Category { get; set; } = [];
    public IEnumerable<ComponentDTO> Component { get; set; } = [];
    public IEnumerable<InventoryDTO> Inventory { get; set; } = [];
    public IEnumerable<NotificationDTO> Notification { get; set; } = [];
    public IEnumerable<RoleDTO> Role { get; set; } = [];
    public IEnumerable<SupplierDTO> Supplier { get; set; } = [];
    public IEnumerable<TaskDTO> Task { get; set; } = [];
    public IEnumerable<UserDTO> User { get; set; } = [];
    public IEnumerable<UserActionDTO> UserAction { get; set; } = [];
    public IEnumerable<UserRoleDTO> UserRole { get; set; } = [];
}
