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
        public UserController(ILogger<UserController> logger, IServiceLocatorService serviceLocator, IServiceMapper serviceMapper)
        {
            _logger = logger;
            _serviceLocator = serviceLocator;
            _serviceMapper = serviceMapper;
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

        // GET: UserService/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserService/Edit/5
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

        // GET: UserService/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserService/Delete/5
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
