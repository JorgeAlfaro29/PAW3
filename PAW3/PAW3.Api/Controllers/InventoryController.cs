using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController(IInventoryBusiness inventoryBusiness) : ControllerBase
    {
        // GET: api/<InventoryController>
        [HttpGet]
        public async Task<IEnumerable<Inventory>> Get()
        {
            return await inventoryBusiness.GetInventory(id: null);

        }

        // GET api/<InventoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InventoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Inventory inventory)
        {
            var result = await inventoryBusiness.SaveInventoryAsync(inventory);
            return result ? Ok(inventory) : BadRequest("No se pudo guardar el inventario.");
        }

        // PUT api/<InventoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Inventory inventory)
        {
            if (id != inventory.InventoryId)
                return BadRequest("El ID no coincide.");

            var result = await inventoryBusiness.SaveInventoryAsync(inventory);
            return result ? Ok(inventory) : BadRequest("No se pudo actualizar el inventario.");
        }

        // DELETE api/<InventoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await inventoryBusiness.DeleteInventoryAsync(id);
            return result ? Ok($"Inventario {id} eliminado correctamente.") : BadRequest("No se pudo eliminar el inventario.");
        }
    }
}
