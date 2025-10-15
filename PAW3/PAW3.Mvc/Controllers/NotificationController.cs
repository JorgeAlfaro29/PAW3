using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.Mvc.Models;
using PAW3.Mvc.ServiceLocator;
using PAW3.ServiceLocator.Helper;

namespace PAW3.Mvc.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly IServiceLocatorService _serviceLocator;
        private readonly IServiceMapper _serviceMapper;
        private readonly INotificationServiceMvc _notificationService;

        public NotificationController(ILogger<NotificationController> logger, IServiceLocatorService serviceLocator, IServiceMapper serviceMapper, INotificationServiceMvc notificationService)
        {
            _logger = logger;
            _serviceLocator = serviceLocator;
            _serviceMapper = serviceMapper;
            _notificationService = notificationService;
        }
        // GET: NotificationController
        public async Task<IActionResult> NotificationList() 
        {
            var notification = await _serviceLocator.GetDataAsync<NotificationDTO>("notifications");
            var homeViewModel = new HomeViewModel()
            {
                Notification = notification
            };
            return View(homeViewModel);
        }

        // GET: NotificationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NotificationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotificationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NotificationDTO notification)
        {
            if (!ModelState.IsValid)
                return View(notification);

            try
            {
                // Usamos el ServiceLocatorService para crear la categoría en la API
                //var createdCategory = await _serviceLocator.CreateAsync("category", category);
                var createdNotification = await _notificationService.CreateAsync(notification);

                // Redirigimos a la lista de categorías
                return RedirectToAction(nameof(NotificationList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando la notification");
                ModelState.AddModelError("", "No se pudo crear la notification.");
                return View(notification);
            }
        }

        // GET: NotificationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var notifications = await _serviceLocator.GetDataAsync<NotificationDTO>("notifications");
            var notification = notifications.FirstOrDefault(c => c.Id == id);

            if (notification == null)
                return NotFound();

            return View(notification);
        }

        // POST: NotificationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NotificationDTO notification)
        {
            if (id != notification.Id)
            {
                ModelState.AddModelError("", "El ID no coincide.");
                return View(notification);
            }

            try
            {
                var result = await _notificationService.UpdateAsync(id, notification);

                if (result)
                    return RedirectToAction(nameof(NotificationList));

                ModelState.AddModelError("", "No se pudo actualizar la notification.");
                return View(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando la notification");
                ModelState.AddModelError("", "Ocurrió un error al actualizar la notification.");
                return View(notification);
            }
        }

        // GET: NotificationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var notifications = await _serviceLocator.GetDataAsync<NotificationDTO>("notifications");
            var notification = notifications.FirstOrDefault(c => c.Id == id);

            if (notification == null)
                return NotFound();

            return View(notification);
        }

        // POST: NotificationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var deleted = await _notificationService.DeleteAsync(id);

                if (deleted)
                    return RedirectToAction(nameof(NotificationList));

                ModelState.AddModelError("", "No se pudo eliminar la notification.");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando la notification con ID {Id}", id);
                ModelState.AddModelError("", "Ocurrió un error al eliminar la notification.");
                return View();
            }
        }
    }
}
