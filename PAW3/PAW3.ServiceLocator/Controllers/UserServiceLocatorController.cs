using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Helper;
using PAW3.ServiceLocator.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.ServiceLocator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceLocatorController(IServiceMapper serviceMapper) : ServiceControllerBase(serviceMapper)
    {
        /*
        // GET: api/<UserServiceLocatorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserServiceLocatorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<UserServiceLocatorController>
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name, [FromBody] UserDTO user)
        {

            var service = await serviceMapper.GetServiceAsync<UserDTO>("user");

            if (service is IUserService userService)
            {
                var created = await userService.CreateAsync(user);
                return Ok(created);
            }

            return BadRequest("Servicio no encontrado.");
        }

        // PUT api/<UserServiceLocatorController>/5
        [HttpPut("{name}/{id}")]
        public async Task<IActionResult> Put(string name, int id, [FromBody] UserDTO user)
        {
            var service = await serviceMapper.GetServiceAsync<UserDTO>(name);

            if (service is IUserService userService)
            {
                if (id != user.UserId)
                    return BadRequest("El ID del body no coincide con el de la URL.");

                var updated = await userService.UpdateAsync(user);

                if (updated)
                    return Ok($"Usuario {id} actualizada correctamente.");

                return BadRequest("No se pudo actualizar el usuario.");
            }

            return BadRequest("Servicio no encontrado.");
        }

        // DELETE api/<UserServiceLocatorController>/5
        [HttpDelete("{name}/{id}")]
        public async Task<IActionResult> Delete(string name, int id)
        {
            var service = await serviceMapper.GetServiceAsync<UserDTO>(name);

            if (service is IUserService userService)
            {
                var deleted = await userService.DeleteAsync(id);

                if (deleted)
                    return Ok($"Usuario {id} eliminada correctamente.");

                return BadRequest("No se pudo eliminar el usuario.");
            }

            return BadRequest("Servicio no encontrado.");
        }
    }
}
