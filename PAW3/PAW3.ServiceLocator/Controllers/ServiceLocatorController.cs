using Microsoft.AspNetCore.Mvc;
using PAW3.Data.DTOs;
using PAW3.Data.Models;
using PAW3.ServiceLocator.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.ServiceLocator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceLocatorController : ControllerBase
    {
        private readonly ITempDataService _tempDataService;
        private readonly IDogDataService _dogDataService;
        private readonly IPeopleDataService _peopleDataService;

        public ServiceLocatorController(ITempDataService tempDataService, IDogDataService dogDataService, IPeopleDataService peopleDataService)
        {
            _tempDataService = tempDataService;
            _dogDataService = dogDataService;
            _peopleDataService = peopleDataService;
        }

        // GET: api/<ServiceLocatorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /*
        // GET api/<ServiceLocatorController>/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<string>> Get(int id)
        {            
            switch (id)
            {
                case 1:
                    return await _tempDataService.GetDataAsync();
                case 2:
                    var result = await _dogDataService.GetDataAsync();
                    return [result];
                case 3:
                    return await _peopleDataService.GetAsEnumerableStringAsync();
                    
                default:
                    return [];
            }

            return [];
        }*/
        
        
        // GET api/<ServiceLocatorController>/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<PersonDTO>> GetPeople(int id)
        {
            switch (id)
            {
                case 1:
                    return await _peopleDataService.GetPeopleAsync<PersonDTO>();

                default:
                    return [];
            }

        }
        

        // POST api/<ServiceLocatorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ServiceLocatorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ServiceLocatorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
