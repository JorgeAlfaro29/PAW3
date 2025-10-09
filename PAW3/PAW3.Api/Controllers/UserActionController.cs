using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActionController(IUserActionBusiness userActionBusiness) : ControllerBase
    {
        // GET: api/<UserActionController>
        [HttpGet]
        public async Task<IEnumerable<UserAction>> Get()
        {
            return await userActionBusiness.GetUserAction(id: null);
        }

        // GET api/<UserActionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserActionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserActionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserActionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
