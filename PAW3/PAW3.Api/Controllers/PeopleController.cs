using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAW3.Data.DTOs;
using PAW3.Data.Models;
using PAW3.Data.People;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly TestdbContext _context;

        public PeopleController()
        {
            _context = new TestdbContext();
        }
        // GET: api/<PeopleController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> Get()
        {

            var peopleData = new PeopleData(_context);
            var people = await peopleData.GetDataPeopleAsync();
            return Ok(people);
        }

        // GET api/<PeopleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PeopleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PeopleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PeopleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
