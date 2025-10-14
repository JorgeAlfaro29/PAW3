using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Models.DTOs;
using PAW3.Mvc.Models;
using PAW3.Mvc.ServiceLocator;
using PAW3.ServiceLocator.Helper;

namespace PAW3.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IServiceLocatorService _serviceLocator;
        private readonly ICategoryServiceMvc _categoryService;
        private readonly IServiceMapper _serviceMapper;
        public CategoryController(ILogger<CategoryController> logger, IServiceLocatorService serviceLocator, IServiceMapper serviceMapper, ICategoryServiceMvc categoryService)
        {
            _logger = logger;
            _serviceLocator = serviceLocator;
            _serviceMapper = serviceMapper;
            _categoryService = categoryService;
        }

        // GET: CategoryController
        public async Task<IActionResult> CategoryList()
        {
            var categories = await _serviceLocator.GetDataAsync<CategoryDTO>("category");
            var homeViewModel = new HomeViewModel()
            {
                Category = categories
            };
            return View(homeViewModel);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if (!ModelState.IsValid)
                return View(category);

            try
            {
                // Usamos el ServiceLocatorService para crear la categoría en la API
                //var createdCategory = await _serviceLocator.CreateAsync("category", category);
                var createdCategory = await _categoryService.CreateAsync(category);

                // Redirigimos a la lista de categorías
                return RedirectToAction(nameof(CategoryList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando la categoría");
                ModelState.AddModelError("", "No se pudo crear la categoría.");
                return View(category);
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _serviceLocator.GetDataAsync<CategoryDTO>("category");
            var category = categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryDTO category)
        {
            if (id != category.CategoryId)
            {
                ModelState.AddModelError("", "El ID no coincide.");
                return View(category);
            }

            try
            {
                var result = await _categoryService.UpdateAsync(id, category);

                if (result)
                    return RedirectToAction(nameof(CategoryList));

                ModelState.AddModelError("", "No se pudo actualizar la categoría.");
                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando la categoría");
                ModelState.AddModelError("", "Ocurrió un error al actualizar la categoría.");
                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var categories = await _serviceLocator.GetDataAsync<CategoryDTO>("category");
            var category = categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var deleted = await _categoryService.DeleteAsync(id);

                if (deleted)
                    return RedirectToAction(nameof(CategoryList));

                ModelState.AddModelError("", "No se pudo eliminar la categoría.");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando la categoría con ID {Id}", id);
                ModelState.AddModelError("", "Ocurrió un error al eliminar la categoría.");
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
