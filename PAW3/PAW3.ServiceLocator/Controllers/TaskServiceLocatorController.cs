using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Helper;
using PAW3.ServiceLocator.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.ServiceLocator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskServiceLocatorController(IServiceMapper serviceMapper) : ServiceControllerBase(serviceMapper)
    {
        /*
        // GET: api/<TaskServiceLocatorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TaskServiceLocatorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<TaskServiceLocatorController>
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name, [FromBody] TaskDTO task)
        {

            var service = await serviceMapper.GetServiceAsync<TaskDTO>("task");

            if (service is ITaskService taskService)
            {
                var created = await taskService.CreateAsync(task);
                return Ok(created);
            }

            return BadRequest("Servicio no encontrado.");
        }

        // PUT api/<TaskServiceLocatorController>/5
        [HttpPut("{name}/{id}")]
        public async Task<IActionResult> Put(string name, int id, [FromBody] TaskDTO task)
        {
            var service = await serviceMapper.GetServiceAsync<TaskDTO>(name);

            if (service is ITaskService taskService)
            {
                if (id != task.Id)
                    return BadRequest("El ID del body no coincide con el de la URL.");

                var updated = await taskService.UpdateAsync(task);

                if (updated)
                    return Ok($"Tarea {id} actualizada correctamente.");

                return BadRequest("No se pudo actualizar la tarea.");
            }

            return BadRequest("Servicio no encontrado.");
        }

        // DELETE api/<TaskServiceLocatorController>/5
        [HttpDelete("{name}/{id}")]
        public async Task<IActionResult> Delete(string name, int id)
        {
            var service = await serviceMapper.GetServiceAsync<TaskDTO>(name);

            if (service is ITaskService taskService)
            {
                var deleted = await taskService.DeleteAsync(id);

                if (deleted)
                    return Ok($"Tarea {id} eliminada correctamente.");

                return BadRequest("No se pudo eliminar la tarea.");
            }

            return BadRequest("Servicio no encontrado.");
        }
    }
}
