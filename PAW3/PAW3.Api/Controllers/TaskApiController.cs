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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TaskApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TaskApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
