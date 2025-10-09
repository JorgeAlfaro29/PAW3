using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PAW3.Models.DTOs
{
    public class NotificationDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("userId")]

        public int UserId { get; set; }
        [JsonPropertyName("message")]

        public string Message { get; set; } = null!;
        [JsonPropertyName("isRead")]

        public bool? IsRead { get; set; }
        [JsonPropertyName("createdAt")]

        public DateTime? CreatedAt { get; set; }
    }
}
