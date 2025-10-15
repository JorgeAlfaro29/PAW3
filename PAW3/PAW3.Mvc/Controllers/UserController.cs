using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.Mvc.Models;
using PAW3.Mvc.ServiceLocator;
using PAW3.ServiceLocator.Helper;

namespace PAW3.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IServiceLocatorService _serviceLocator;
        private readonly IServiceMapper _serviceMapper;
        private readonly IUserServiceMvc _userService;
        public UserController(ILogger<UserController> logger, IServiceLocatorService serviceLocator, IServiceMapper serviceMapper, IUserServiceMvc userService)
        {
            _logger = logger;
            _serviceLocator = serviceLocator;
            _serviceMapper = serviceMapper;
            _userService = userService;
        }
        // GET: UserService
        public async Task<IActionResult> UserList()
        {
            var user = await _serviceLocator.GetDataAsync<UserDTO>("user");
            var homeViewModel = new HomeViewModel()
            {
                User = user
            };
            return View(homeViewModel);
        }

        // GET: UserService/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserService/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserService/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDTO user)
        {
            if (!ModelState.IsValid)
                return View(user);

            try
            {
                // Usamos el ServiceLocatorService para crear la categoría en la API
                //var createdCategory = await _serviceLocator.CreateAsync("category", category);
                var createdUser = await _userService.CreateAsync(user);

                // Redirigimos a la lista de categorías
                return RedirectToAction(nameof(UserList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando el usuario");
                ModelState.AddModelError("", "No se pudo crear el usuario.");
                return View(user);
            }
        }

        // GET: UserService/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var users = await _serviceLocator.GetDataAsync<UserDTO>("user");
            var user = users.FirstOrDefault(c => c.UserId == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: UserService/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserDTO user)
        {
            if (id != user.UserId)
            {
                ModelState.AddModelError("", "El ID no coincide.");
                return View(user);
            }

            try
            {
                var result = await _userService.UpdateAsync(id, user);

                if (result)
                    return RedirectToAction(nameof(UserList));

                ModelState.AddModelError("", "No se pudo actualizar el usuario.");
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando el usuario");
                ModelState.AddModelError("", "Ocurrió un error al actualizar el usuario.");
                return View(user);
            }
        }

        // GET: UserService/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var users = await _serviceLocator.GetDataAsync<UserDTO>("user");
            var user = users.FirstOrDefault(c => c.UserId == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: UserService/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var deleted = await _userService.DeleteAsync(id);

                if (deleted)
                    return RedirectToAction(nameof(UserList));

                ModelState.AddModelError("", "No se pudo eliminar el usuario.");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando el usuario con ID {Id}", id);
                ModelState.AddModelError("", "Ocurrió un error al eliminar el usuario.");
                return View();
            }
        }
    }
}
