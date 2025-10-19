using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PAW3.Data.Models;

namespace PAW3.Models.DTOs
{

    /*
     Patron de Diseño
     Data Transfer Object (DTO)

     Segun la investigacion realizada el patron de diseño DTO se utiliza para transferir datos entre diferentes capas de una aplicacion, para que de esta forma la clase que se creo relacionada a la base de datos
     no este expuesta a otras capas y no se muestre explicitamente en la aplicacion, al usar un DTO se crea un copia de la clase original pero con datos unicamente para una transferencia de datos, se puede utilizar
     en todo el codigo de manera que no genera un riesgo que la clase orifinal sea inyectada con datos ya que se harian cambios directos a la base de datos en cambio con una DTO se hace la copia y no interactua
     directamente con la base de datos. Eso si tiene que sea una copia exacta de la clase original para que no haya errores al momento de hacer la transferencia de datos, y esta clase puede ser utilizada por todo el 
     sistema sin ningun problema ya que fue creada para lo mismo.
     */


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
