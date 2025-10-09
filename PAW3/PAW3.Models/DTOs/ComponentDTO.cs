using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PAW3.Models.DTOs
{
    public class ComponentDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]

        public string Name { get; set; } = null!;
        [JsonPropertyName("content")]

        public string Content { get; set; } = null!;
    }
}
