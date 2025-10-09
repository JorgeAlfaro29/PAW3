using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PAW3.Data.Models;

namespace PAW3.Models.DTOs
{
    public class InventoryDTO
    {
        [JsonPropertyName("inventoryId")]
        public int InventoryId { get; set; }
        [JsonPropertyName("unitPrice")]

        public decimal? UnitPrice { get; set; }
        [JsonPropertyName("unitsInStock")]

        public int? UnitsInStock { get; set; }
        [JsonPropertyName("lastUpdated")]

        public DateTime? LastUpdated { get; set; }
        [JsonPropertyName("productId")]

        public int? ProductId { get; set; }
        [JsonPropertyName("dateAdded")]

        public DateTime? DateAdded { get; set; }
        [JsonPropertyName("modifiedBy")]

        public string? ModifiedBy { get; set; }
        [JsonPropertyName("products")]

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
