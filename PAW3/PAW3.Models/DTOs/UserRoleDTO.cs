using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PAW3.Models.DTOs
{
    public class UserRoleDTO
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("roldId")]
        public int? RoldId { get; set; }
        [JsonPropertyName("userId")]

        public int? UserId { get; set; }
    }
}
