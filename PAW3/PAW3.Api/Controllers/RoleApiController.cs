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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RoleApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoleApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
