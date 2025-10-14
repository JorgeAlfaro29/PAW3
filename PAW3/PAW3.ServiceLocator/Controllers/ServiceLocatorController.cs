using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PAW3.Data.Models;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Helper;
using PAW3.ServiceLocator.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.ServiceLocator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceLocatorController(IServiceMapper serviceMapper) : ServiceControllerBase(serviceMapper)
{
    // GET api/<ServiceLocatorController>/5
    [HttpGet("{name}")]
    public async Task<IEnumerable<object>> Get(string name)
    {
        if (ServiceResolvers.TryGetValue(name.ToLower(), out var resolver))
            return await resolver();

        return [];
    }

    /*
    // POST api/ServiceLocator/category
    [HttpPost("{name}")]
    public async Task<IActionResult> Post(string name, [FromBody] CategoryDTO category)
    {
           
        var service = await serviceMapper.GetServiceAsync<CategoryDTO>("category");

        if (service is ICategoryService categoryService)
        {
            var created = await categoryService.CreateAsync(category);
            return Ok(created);
        }

        return BadRequest("Servicio no encontrado.");
    }*/
    

    
    /*
    
    // PUT api/ServiceLocator/{name}/{id}
    [HttpPut("{name}/{id}")]
    public async Task<IActionResult> Put(string name, int id, [FromBody] CategoryDTO category)
    {
        if (name.ToLower() != "category")
            return BadRequest("Este endpoint solo soporta 'category' por ahora.");

        var service = await serviceMapper.GetServiceAsync<CategoryDTO>("category");

        if (service is ICategoryService categoryService)
        {
            var updated = await categoryService.UpdateAsync(category);

            if (updated != null)
                return Ok(updated);

            return BadRequest("No se pudo actualizar la categoría.");
        }

        return BadRequest("Servicio no encontrado.");
    }

    // DELETE api/ServiceLocator/{name}/{id}
    [HttpDelete("{name}/{id}")]
    public async Task<IActionResult> Delete(string name, int id)
    {
        if (name.ToLower() != "category")
            return BadRequest("Este endpoint solo soporta 'category' por ahora.");

        var service = await serviceMapper.GetServiceAsync<CategoryDTO>("category");

        if (service is ICategoryService categoryService)
        {
            var success = await categoryService.DeleteAsync(id);

            if (success)
                return Ok($"Categoría con ID {id} eliminada correctamente.");

            return BadRequest("No se pudo eliminar la categoría.");
        }

        return BadRequest("Servicio no encontrado.");
    }
    */
}
