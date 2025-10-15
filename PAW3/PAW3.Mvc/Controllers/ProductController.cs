using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.Mvc.Models;
using PAW3.Mvc.ServiceLocator;
using PAW3.ServiceLocator.Helper;
using PAW3.ServiceLocator.Services;

namespace PAW3.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IServiceLocatorService _serviceLocator;
        private readonly IProductServiceMvc _productService;
        private readonly IServiceMapper _serviceMapper;

        public ProductController(ILogger<ProductController> logger, IServiceLocatorService serviceLocator, IServiceMapper serviceMapper, IProductServiceMvc productService)
        {
            _logger = logger;
            _serviceLocator = serviceLocator;
            _serviceMapper = serviceMapper;
            _productService = productService;
        }
        // GET: ProductController
        public async Task<IActionResult> ProductList()
        {
            var products = await _serviceLocator.GetDataAsync<ProductDTO>("product");
            var homeViewModel = new HomeViewModel()
            {
                Products = products
            };
            return View(homeViewModel);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return View(product);

            try
            {
                // Usamos el ServiceLocatorService para crear la categoría en la API
                //var createdCategory = await _serviceLocator.CreateAsync("category", category);
                var createdCategory = await _productService.CreateAsync(product);

                // Redirigimos a la lista de categorías
                return RedirectToAction(nameof(ProductList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando el producto");
                ModelState.AddModelError("", "No se pudo crear el producto.");
                return View(product);
            }
        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _serviceLocator.GetDataAsync<ProductDTO>("product");
            var product = products.FirstOrDefault(c => c.ProductId == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDTO product)
        {
            if (id != product.ProductId)
            {
                ModelState.AddModelError("", "El ID no coincide.");
                return View(product);
            }

            try
            {
                var result = await _productService.UpdateAsync(id, product);

                if (result)
                    return RedirectToAction(nameof(ProductList));

                ModelState.AddModelError("", "No se pudo actualizar el producto.");
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando el producto");
                ModelState.AddModelError("", "Ocurrió un error al actualizar el producto.");
                return View(product);
            }
        }

        // GET: ProductController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var products = await _serviceLocator.GetDataAsync<ProductDTO>("product");
            var product = products.FirstOrDefault(c => c.ProductId == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var deleted = await _productService.DeleteAsync(id);

                if (deleted)
                    return RedirectToAction(nameof(ProductList));

                ModelState.AddModelError("", "No se pudo eliminar el producto.");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando el producto con ID {Id}", id);
                ModelState.AddModelError("", "Ocurrió un error al eliminar el producto.");
                return View();
            }
        }
    }
}
