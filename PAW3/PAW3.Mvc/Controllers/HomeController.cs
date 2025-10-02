using Microsoft.AspNetCore.Mvc;
using PAW3.Mvc.Models;
using PAW3.Mvc.ServiceLocator;
using System.Diagnostics;

namespace PAW3.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceLocatorService _serviceLocator;

        public HomeController(ILogger<HomeController> logger, IServiceLocatorService serviceLocator)
        {
            _logger = logger;
            _serviceLocator = serviceLocator;
        }

        public async Task<IActionResult> Index()
        {
            //var names = await _serviceLocator.GetDataAsync("1");
            //var dog = await _serviceLocator.GetDataAsync("2");
            var people = await _serviceLocator.GetDataAsync("1");
            var homeViewModel = new HomeViewModel()
            {
                Title = "Tarea #1",
                People = people,
                //Dog = dog
            };
            return View(homeViewModel);
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
