using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Helper;
using PAW3.ServiceLocator.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.ServiceLocator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryServiceLocatorController(IServiceMapper serviceMapper) : ServiceControllerBase(serviceMapper)
    {
        /*
        // GET: api/<InventoryServiceLocatorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<InventoryServiceLocatorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<InventoryServiceLocatorController>
        [HttpPost("{name}")]
        public async Task<IActionResult> Post(string name, [FromBody] InventoryDTO inventory)
        {

            var service = await serviceMapper.GetServiceAsync<InventoryDTO>("inventory");

            if (service is IInventoryService inventoryService)
            {
                var created = await inventoryService.CreateAsync(inventory);
                return Ok(created);
            }

            return BadRequest("Servicio no encontrado.");
        }

        // PUT api/<InventoryServiceLocatorController>/5
        [HttpPut("{name}/{id}")]
        public async Task<IActionResult> Put(string name, int id, [FromBody] InventoryDTO inventory)
        {
            var service = await serviceMapper.GetServiceAsync<InventoryDTO>(name);

            if (service is IInventoryService inventoryService)
            {
                if (id != inventory.InventoryId)
                    return BadRequest("El ID del body no coincide con el de la URL.");

                var updated = await inventoryService.UpdateAsync(inventory);

                if (updated)
                    return Ok($"Inventario {id} actualizado correctamente.");

                return BadRequest("No se pudo actualizar el inventario.");
            }

            return BadRequest("Servicio no encontrado.");
        }

        // DELETE api/<InventoryServiceLocatorController>/5
        [HttpDelete("{name}/{id}")]
        public async Task<IActionResult> Delete(string name, int id)
        {
            var service = await serviceMapper.GetServiceAsync<InventoryDTO>(name);

            if (service is IInventoryService inventoryService)
            {
                var deleted = await inventoryService.DeleteAsync(id);

                if (deleted)
                    return Ok($"Inventario {id} eliminado correctamente.");

                return BadRequest("No se pudo eliminar el inventario.");
            }

            return BadRequest("Servicio no encontrado.");
        }
    }
}
