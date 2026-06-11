using Microsoft.AspNetCore.Mvc;
using MVC_Veterinario.Data;
using MVC_Veterinario.Models;
using System.Diagnostics;

namespace MVC_Veterinario.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VetContext _context;

        public HomeController(ILogger<HomeController> logger, VetContext context)
        {
            _logger = logger;
            _context = context;
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
