using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System.Diagnostics;

namespace SalesWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SeedingService _seedingService;

        public HomeController(ILogger<HomeController> logger, SeedingService seedingService)
        {
            _seedingService = seedingService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Seed()
        {
            _seedingService.Seed();
            return Ok("Dados semeados com sucesso!");
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
