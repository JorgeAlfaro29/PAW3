using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Helper;
using PAW3.ServiceLocator.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.ServiceLocator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryServiceLocatorController(IServiceMapper serviceMapper) : ServiceControllerBase(serviceMapper) 
    {
        /*
        // GET: api/<CategoryServiceLocatorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CategoryServiceLocatorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        /*
          ***Patron de Diseño
          Data Transfer Object (DTO) 

         Como forma de ejemplo podemos ver como en esta api se utiliza el DTO de category, y hace la funcion de tranferencia de datos entre diferentes capas de la aplicacion y asi no exponer la clase original 
         que tiene interaccion con el context y base datos. 

         */

        // POST api/ServiceLocator/category
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name, [FromBody] CategoryDTO category)
        {

            var service = await serviceMapper.GetServiceAsync<CategoryDTO>("category");

            if (service is ICategoryService categoryService)
            {
                var created = await categoryService.CreateAsync(category);
                return Ok(created);
            }

            return BadRequest("Servicio no encontrado.");
        }

        // PUT api/CategoryServiceLocator/category/5
        [HttpPut("{name}/{id}")]
        public async Task<IActionResult> Put(string name, int id, [FromBody] CategoryDTO category)
        {
            var service = await serviceMapper.GetServiceAsync<CategoryDTO>(name);

            if (service is ICategoryService categoryService)
            {
                if (id != category.CategoryId)
                    return BadRequest("El ID del body no coincide con el de la URL.");

                var updated = await categoryService.UpdateAsync(category);

                if (updated)
                    return Ok($"Categoría {id} actualizada correctamente.");

                return BadRequest("No se pudo actualizar la categoría.");
            }

            return BadRequest("Servicio no encontrado.");
        }

        // DELETE api/CategoryServiceLocator/category/5
        [HttpDelete("{name}/{id}")]
        public async Task<IActionResult> Delete(string name, int id)
        {
            var service = await serviceMapper.GetServiceAsync<CategoryDTO>(name);

            if (service is ICategoryService categoryService)
            {
                var deleted = await categoryService.DeleteAsync(id);

                if (deleted)
                    return Ok($"Categoría {id} eliminada correctamente.");

                return BadRequest("No se pudo eliminar la categoría.");
            }

            return BadRequest("Servicio no encontrado.");
        }
    }
}

