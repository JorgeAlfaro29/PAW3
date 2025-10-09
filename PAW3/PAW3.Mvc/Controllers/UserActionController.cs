using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAW3.Models.DTOs;
using PAW3.Mvc.Models;
using PAW3.Mvc.ServiceLocator;
using PAW3.ServiceLocator.Helper;

namespace PAW3.Mvc.Controllers
{
    public class UserActionController : Controller
    {
        private readonly ILogger<UserActionController> _logger;
        private readonly IServiceLocatorService _serviceLocator;
        private readonly IServiceMapper _serviceMapper;

        public UserActionController(ILogger<UserActionController> logger, IServiceLocatorService serviceLocator, IServiceMapper serviceMapper)
        {
            _logger = logger;
            _serviceLocator = serviceLocator;
            _serviceMapper = serviceMapper;

        }
        // GET: UserActionController
        public async Task<IActionResult> UserActionList()
        {
            var userAction = await _serviceLocator.GetDataAsync<UserActionDTO>("useraction");
            var homeViewModel = new HomeViewModel()
            {
                UserAction = userAction
            };
            return View(homeViewModel);
        }

        // GET: UserActionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserActionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserActionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserActionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserActionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserActionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserActionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
