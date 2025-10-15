using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskApiController(ITaskBusiness taskBusiness) : ControllerBase
    {
        // GET: api/<TaskApiController>
        [HttpGet]
        public async Task<IEnumerable<Tasks>> Get()
        {
            return await taskBusiness.GetTask(id: null);
        }

        // GET api/<TaskApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TaskApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Tasks task)
        {
            var result = await taskBusiness.SaveTaskAsync(task);
            return result ? Ok(task) : BadRequest("No se pudo guardar la tarea.");
        }

        // PUT api/<TaskApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Tasks task)
        {
            if (id != task.Id)
                return BadRequest("El ID no coincide.");

            var result = await taskBusiness.SaveTaskAsync(task);
            return result ? Ok(task) : BadRequest("No se pudo actualizar la tarea.");
        }

        // DELETE api/<TaskApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await taskBusiness.DeleteTaskAsync(id);
            return result ? Ok($"Tarea {id} eliminada correctamente.") : BadRequest("No se pudo eliminar la tarea.");
        }
    }
}
