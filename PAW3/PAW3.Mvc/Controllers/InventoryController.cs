using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.Mvc.Models;
using PAW3.Mvc.ServiceLocator;
using PAW3.ServiceLocator.Helper;

namespace PAW3.Mvc.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IServiceLocatorService _serviceLocator;
        private readonly IInventoryServiceMvc _inventoryService;
        private readonly IServiceMapper _serviceMapper;

        public InventoryController(ILogger<InventoryController> logger, IServiceLocatorService serviceLocator, IInventoryServiceMvc inventoryService, IServiceMapper serviceMapper)
        {
            _logger = logger;
            _serviceLocator = serviceLocator;
            _serviceMapper = serviceMapper;
            _inventoryService = inventoryService;
        }
        // GET: InventoryController
        public async Task<IActionResult> InventoryList()
        {
            var inventory = await _serviceLocator.GetDataAsync<InventoryDTO>("inventory");
            var homeViewModel = new HomeViewModel()
            {
                Inventory = inventory
            };
            return View(homeViewModel);
        }

        // GET: InventoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InventoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryDTO inventory)
        {
            if (!ModelState.IsValid)
                return View(inventory);

            try
            {
                // Usamos el ServiceLocatorService para crear la categoría en la API
                //var createdCategory = await _serviceLocator.CreateAsync("category", category);
                var createdInventory = await _inventoryService.CreateAsync(inventory);

                // Redirigimos a la lista de categorías
                return RedirectToAction(nameof(InventoryList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando el inventario");
                ModelState.AddModelError("", "No se pudo crear el inventario.");
                return View(inventory);
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var inventories = await _serviceLocator.GetDataAsync<InventoryDTO>("inventory");
            var inventory = inventories.FirstOrDefault(c => c.InventoryId == id);

            if (inventory == null)
                return NotFound();

            return View(inventory);
        }

        // POST: InventoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InventoryDTO inventory)
        {
            if (id != inventory.InventoryId)
            {
                ModelState.AddModelError("", "El ID no coincide.");
                return View(inventory);
            }

            try
            {
                var result = await _inventoryService.UpdateAsync(id, inventory);

                if (result)
                    return RedirectToAction(nameof(InventoryList));

                ModelState.AddModelError("", "No se pudo actualizar el inventario.");
                return View(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando el inventario");
                ModelState.AddModelError("", "Ocurrió un error al actualizar el inventario.");
                return View(inventory);
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var inventories = await _serviceLocator.GetDataAsync<InventoryDTO>("inventory");
            var inventory = inventories.FirstOrDefault(c => c.InventoryId == id);

            if (inventory == null)
                return NotFound();

            return View(inventory);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var deleted = await _inventoryService.DeleteAsync(id);

                if (deleted)
                    return RedirectToAction(nameof(InventoryList));

                ModelState.AddModelError("", "No se pudo eliminar el inventario.");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando el inventario con ID {Id}", id);
                ModelState.AddModelError("", "Ocurrió un error al eliminar el inventario.");
                return View();
            }
        }
    }
}
