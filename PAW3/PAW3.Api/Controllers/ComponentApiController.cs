using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentApiController(IComponentRepository componentRepository) : ControllerBase
    {
        // GET: api/<ComponentApiController>
        [HttpGet]
        public async Task<IEnumerable<Component>> Get()
        {
            return await componentRepository.GetComponent(id: null);
        }

        // GET api/<ComponentApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoryApiController>/5
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Component component)
        {
            var result = await componentRepository.SaveComponentAsync(component);
            return result ? Ok(component) : BadRequest("No se pudo guardar la categoría.");
        }

        // PUT api/<CategoryApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Component component)
        {
            if (id != component.Id)
                return BadRequest("El ID no coincide.");

            var result = await componentRepository.SaveComponentAsync(component);
            return result ? Ok(component) : BadRequest("No se pudo actualizar la categoría.");
        }

        // DELETE api/<ComponentApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await componentRepository.DeleteComponentAsync(id);
            return result ? Ok($"Categoría {id} eliminada correctamente.") : BadRequest("No se pudo eliminar la categoría.");
        }
    }
}
