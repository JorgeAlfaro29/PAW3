using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleApiController (IRoleBusiness roleBusiness): ControllerBase
    {
        // GET: api/<RoleApiController>
        [HttpGet]
        public async Task<IEnumerable<Role>> Get()
        {
            return await roleBusiness.GetRole(id: null);
        }

        // GET api/<RoleApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoleApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Role role)
        {
            var result = await roleBusiness.SaveRoleAsync(role);
            return result ? Ok(role) : BadRequest("No se pudo guardar el rol.");
        }

        // PUT api/<RoleApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Role role)
        {
            if (id != role.RoleId)
                return BadRequest("El ID no coincide.");

            var result = await roleBusiness.SaveRoleAsync(role);
            return result ? Ok(role) : BadRequest("No se pudo actualizar el rol.");
        }

        // DELETE api/<RoleApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await roleBusiness.DeleteRoleAsync(id);
            return result ? Ok($"Role {id} eliminada correctamente.") : BadRequest("No se pudo eliminar el rol.");
        }
    }
}
