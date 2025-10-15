using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Helper;
using PAW3.ServiceLocator.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.ServiceLocator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleServiceLocatorController(IServiceMapper serviceMapper) : ServiceControllerBase(serviceMapper)
    {
        /*
        // GET: api/<RoleServiceLocatorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RoleServiceLocatorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<RoleServiceLocatorController>
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name, [FromBody] RoleDTO role)
        {

            var service = await serviceMapper.GetServiceAsync<RoleDTO>("role");

            if (service is IRoleService roleService)
            {
                var created = await roleService.CreateAsync(role);
                return Ok(created);
            }

            return BadRequest("Servicio no encontrado.");
        }

        // PUT api/<RoleServiceLocatorController>/5
        [HttpPut("{name}/{id}")]
        public async Task<IActionResult> Put(string name, int id, [FromBody] RoleDTO role)
        {
            var service = await serviceMapper.GetServiceAsync<RoleDTO>(name);

            if (service is IRoleService roleService)
            {
                if (id != role.RoleId)
                    return BadRequest("El ID del body no coincide con el de la URL.");

                var updated = await roleService.UpdateAsync(role);

                if (updated)
                    return Ok($"Rol {id} actualizado correctamente.");

                return BadRequest("No se pudo actualizar el rol.");
            }

            return BadRequest("Servicio no encontrado.");
        }

        // DELETE api/<RoleServiceLocatorController>/5
        [HttpDelete("{name}/{id}")]
        public async Task<IActionResult> Delete(string name, int id)
        {
            var service = await serviceMapper.GetServiceAsync<RoleDTO>(name);

            if (service is IRoleService roleService)
            {
                var deleted = await roleService.DeleteAsync(id);

                if (deleted)
                    return Ok($"Role {id} eliminado correctamente.");

                return BadRequest("No se pudo eliminar el rol.");
            }

            return BadRequest("Servicio no encontrado.");
        }
    }
}
