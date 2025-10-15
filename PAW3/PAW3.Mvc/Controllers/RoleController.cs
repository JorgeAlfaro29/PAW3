using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.Mvc.Models;
using PAW3.Mvc.ServiceLocator;
using PAW3.ServiceLocator.Helper;

namespace PAW3.Mvc.Controllers
{
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IServiceLocatorService _serviceLocator;
        private readonly IServiceMapper _serviceMapper;
        private readonly IRoleServiceMvc _roleService;
        public RoleController(ILogger<RoleController> logger, IServiceLocatorService serviceLocator, IServiceMapper serviceMapper, IRoleServiceMvc roleService)
        {
            _logger = logger;
            _serviceLocator = serviceLocator;
            _serviceMapper = serviceMapper;
            _roleService = roleService;
        }
        // GET: RoleController
        public async Task<IActionResult> RoleList()
        {
            var role = await _serviceLocator.GetDataAsync<RoleDTO>("role");
            var homeViewModel = new HomeViewModel()
            {
                Role = role
            };
            return View(homeViewModel);
        }

        // GET: RoleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleDTO role)
        {
            if (!ModelState.IsValid)
                return View(role);

            try
            {
                // Usamos el ServiceLocatorService para crear la categoría en la API
                //var createdCategory = await _serviceLocator.CreateAsync("category", category);
                var createdRole = await _roleService.CreateAsync(role);

                // Redirigimos a la lista de categorías
                return RedirectToAction(nameof(RoleList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando el role");
                ModelState.AddModelError("", "No se pudo crear el role.");
                return View(role);
            }
        }

        // GET: RoleController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var roles = await _serviceLocator.GetDataAsync<RoleDTO>("role");
            var role = roles.FirstOrDefault(c => c.RoleId == id);

            if (role == null)
                return NotFound();

            return View(role);
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoleDTO role)
        {
            if (id != role.RoleId)
            {
                ModelState.AddModelError("", "El ID no coincide.");
                return View(role);
            }

            try
            {
                var result = await _roleService.UpdateAsync(id, role);

                if (result)
                    return RedirectToAction(nameof(RoleList));

                ModelState.AddModelError("", "No se pudo actualizar el role.");
                return View(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando el role");
                ModelState.AddModelError("", "Ocurrió un error al actualizar el role.");
                return View(role);
            }
        }

        // GET: RoleController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var roles = await _serviceLocator.GetDataAsync<RoleDTO>("role");
            var role = roles.FirstOrDefault(c => c.RoleId == id);

            if (role == null)
                return NotFound();

            return View(role);
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var deleted = await _roleService.DeleteAsync(id);

                if (deleted)
                    return RedirectToAction(nameof(RoleList));

                ModelState.AddModelError("", "No se pudo eliminar el role.");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando el role con ID {Id}", id);
                ModelState.AddModelError("", "Ocurrió un error al eliminar el role.");
                return View();
            }
        }
    }
}
