using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Models;
using PAW3.Models.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController(ICategoryBusiness categoryBusiness) : ControllerBase
    {
        // GET: api/<CategoryApiController>
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await categoryBusiness.GetCategory(id:null); 
        }

        // GET api/<CategoryApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoryApiController>/5
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            var result = await categoryBusiness.SaveCategoryAsync(category);
            return result ? Ok(category) : BadRequest("No se pudo guardar la categoría.");
        }

        // PUT api/<CategoryApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category category)
        {
            if (id != category.CategoryId)
                return BadRequest("El ID no coincide.");

            var result = await categoryBusiness.SaveCategoryAsync(category);
            return result ? Ok(category) : BadRequest("No se pudo actualizar la categoría.");
        }

        // DELETE api/<CategoryApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await categoryBusiness.DeleteCategoryAsync(id);
            return result ? Ok($"Categoría {id} eliminada correctamente.") : BadRequest("No se pudo eliminar la categoría.");
        }
    }
}
