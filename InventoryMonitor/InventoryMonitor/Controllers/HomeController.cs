using System.Diagnostics;
using InventoryMonitor.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMonitor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;
        
        public string GetConnectionString()
        {
            // Retrieve the connection string from the configuration
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return connectionString;
        }



        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }




        public IActionResult Index()
        {
            return View();
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
