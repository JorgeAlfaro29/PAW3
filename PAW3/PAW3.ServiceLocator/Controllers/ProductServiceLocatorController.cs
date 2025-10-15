using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Helper;
using PAW3.ServiceLocator.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.ServiceLocator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductServiceLocatorController(IServiceMapper serviceMapper) : ServiceControllerBase(serviceMapper)
    {
        /*
        // GET: api/<ProductServiceLocatorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductServiceLocatorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<ProductServiceLocatorController>
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name, [FromBody] ProductDTO product)
        {

            var service = await serviceMapper.GetServiceAsync<ProductDTO>("product");

            if (service is IProductService productService)
            {
                var created = await productService.CreateAsync(product);
                return Ok(created);
            }

            return BadRequest("Servicio no encontrado.");
        }

        // PUT api/<ProductServiceLocatorController>/5
        [HttpPut("{name}/{id}")]
        public async Task<IActionResult> Put(string name, int id, [FromBody] ProductDTO product)
        {
            var service = await serviceMapper.GetServiceAsync<ProductDTO>(name);

            if (service is IProductService productService)
            {
                if (id != product.ProductId)
                    return BadRequest("El ID del body no coincide con el de la URL.");

                var updated = await productService.UpdateAsync(product);

                if (updated)
                    return Ok($"Producto {id} actualizado correctamente.");

                return BadRequest("No se pudo actualizar el producto.");
            }

            return BadRequest("Servicio no encontrado.");
        }

        // DELETE api/<ProductServiceLocatorController>/5
        [HttpDelete("{name}/{id}")]
        public async Task<IActionResult> Delete(string name, int id)
        {
            var service = await serviceMapper.GetServiceAsync<ProductDTO>(name);

            if (service is IProductService productService)
            {
                var deleted = await productService.DeleteAsync(id);

                if (deleted)
                    return Ok($"Producto {id} eliminado correctamente.");

                return BadRequest("No se pudo eliminar el producto.");
            }

            return BadRequest("Servicio no encontrado.");
        }
    }
}
