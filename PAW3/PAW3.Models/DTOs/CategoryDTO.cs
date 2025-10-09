using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PAW3.Data.Models;

namespace PAW3.Models.DTOs
{
    public class CategoryDTO
    {
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }
        [JsonPropertyName("categoryName")]

        public string? CategoryName { get; set; }
        [JsonPropertyName("description")]

        public string? Description { get; set; }
        [JsonPropertyName("lastModified")]

        public DateTime? LastModified { get; set; }
        [JsonPropertyName("modifiedBy")]

        public string? ModifiedBy { get; set; }
        [JsonPropertyName("products")]

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
