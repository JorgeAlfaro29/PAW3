using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.Mvc.Models;
using PAW3.Mvc.ServiceLocator;
using PAW3.ServiceLocator.Helper;

namespace PAW3.Mvc.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IServiceLocatorService _serviceLocator;
        private readonly IServiceMapper _serviceMapper;
        private readonly ITaskServiceMvc _taskService;

        public TaskController(ILogger<TaskController> logger, IServiceLocatorService serviceLocator, IServiceMapper serviceMapper, ITaskServiceMvc taskService)
        {
            _logger = logger;
            _serviceLocator = serviceLocator;
            _serviceMapper = serviceMapper;
            _taskService = taskService;
        }
        // GET: TaskController
        public async Task<IActionResult> TaskList()
        {
            var task = await _serviceLocator.GetDataAsync<TaskDTO>("task");
            var homeViewModel = new HomeViewModel()
            {
                Task = task
            };
            return View(homeViewModel);
        }

        // GET: TaskController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TaskController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskDTO task)
        {
            if (!ModelState.IsValid)
                return View(task);

            try
            {
                // Usamos el ServiceLocatorService para crear la categoría en la API
                //var createdCategory = await _serviceLocator.CreateAsync("category", category);
                var createdTask = await _taskService.CreateAsync(task);

                // Redirigimos a la lista de categorías
                return RedirectToAction(nameof(TaskList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando la tarea");
                ModelState.AddModelError("", "No se pudo crear la tarea.");
                return View(task);
            }
        }

        // GET: TaskController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tasks = await _serviceLocator.GetDataAsync<TaskDTO>("task");
            var task = tasks.FirstOrDefault(c => c.Id == id);

            if (task == null)
                return NotFound();

            return View(task);
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskDTO task)
        {
            if (id != task.Id)
            {
                ModelState.AddModelError("", "El ID no coincide.");
                return View(task);
            }

            try
            {
                var result = await _taskService.UpdateAsync(id, task);

                if (result)
                    return RedirectToAction(nameof(TaskList));

                ModelState.AddModelError("", "No se pudo actualizar la tarea.");
                return View(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando la tarea");
                ModelState.AddModelError("", "Ocurrió un error al actualizar la tarea.");
                return View(task);
            }
        }

        // GET: TaskController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tasks = await _serviceLocator.GetDataAsync<TaskDTO>("task");
            var task = tasks.FirstOrDefault(c => c.Id == id);

            if (task == null)
                return NotFound();

            return View(task);
        }

        // POST: TaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var deleted = await _taskService.DeleteAsync(id);

                if (deleted)
                    return RedirectToAction(nameof(TaskList));

                ModelState.AddModelError("", "No se pudo eliminar la tarea.");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando la tarea con ID {Id}", id);
                ModelState.AddModelError("", "Ocurrió un error al eliminar la tarea.");
                return View();
            }
        }
    }
}
