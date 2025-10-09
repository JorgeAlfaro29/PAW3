using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleApiController(IUserRoleBusiness userRoleBusiness) : ControllerBase
    {
        // GET: api/<UserRoleApiController>
        [HttpGet]
        public async Task<IEnumerable<UserRole>> Get()
        {
            return await userRoleBusiness.GetUserRole(id:null);
        }

        // GET api/<UserRoleApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserRoleApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserRoleApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserRoleApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
