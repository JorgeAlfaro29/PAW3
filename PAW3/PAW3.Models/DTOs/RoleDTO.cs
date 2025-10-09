using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PAW3.Models.DTOs
{
    public class RoleDTO
    {
        [JsonPropertyName("roleId")]
        public int RoleId { get; set; }
        [JsonPropertyName("roleName")]

        public string? RoleName { get; set; }
    }
}
