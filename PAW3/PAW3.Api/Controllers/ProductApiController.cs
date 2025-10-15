using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController(IProductBusiness productBusiness) : ControllerBase
    {
        // GET: api/<ProductApiController>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await productBusiness.GetProducts(id: null);
        }

        // GET api/<ProductApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            var result = await productBusiness.SaveProductAsync(product);
            return result ? Ok(product) : BadRequest("No se pudo guardar el producto.");
        }

        // PUT api/<ProductApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
                return BadRequest("El ID no coincide.");

            var result = await productBusiness.SaveProductAsync(product);
            return result ? Ok(product) : BadRequest("No se pudo actualizar  el producto.");
        }

        // DELETE api/<ProductApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await productBusiness.DeleteProductAsync(id);
            return result ? Ok($"Producto {id} eliminada correctamente.") : BadRequest("No se pudo eliminar el producto.");
        }
    }
}
