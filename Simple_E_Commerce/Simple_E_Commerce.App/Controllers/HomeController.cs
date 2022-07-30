using Microsoft.AspNetCore.Mvc;
using Simple_E_Commerce.App.Models;
using Simple_E_Commerce.Data.Context;
using System.Diagnostics;

namespace Simple_E_Commerce.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SimpleEcommerceDbContext _simpleEcommerceDbContext;

        public HomeController(ILogger<HomeController> logger, SimpleEcommerceDbContext simpleEcommerceDbContext)
        {
            _logger = logger;
            _simpleEcommerceDbContext = simpleEcommerceDbContext;
        }

        public IActionResult Index()
        {
            var products = _simpleEcommerceDbContext.Products.ToList();

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}