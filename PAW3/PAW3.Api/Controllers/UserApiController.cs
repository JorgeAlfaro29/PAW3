using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController(IUserBusiness userBusiness) : ControllerBase
    {
        // GET: api/<UserApiController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await userBusiness.GetUser(id: null);
        }

        // GET api/<UserApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            var result = await userBusiness.SaveUserAsync(user);
            return result ? Ok(user) : BadRequest("No se pudo guardar el usuario.");
        }

        // PUT api/<UserApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            if (id != user.UserId)
                return BadRequest("El ID no coincide.");

            var result = await userBusiness.SaveUserAsync(user);
            return result ? Ok(user) : BadRequest("No se pudo actualizar el usuario.");
        }

        // DELETE api/<UserApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await userBusiness.DeleteUserAsync(id);
            return result ? Ok($"Usuario {id} eliminado correctamente.") : BadRequest("No se pudo eliminar el usuario.");
        }
    }
}
