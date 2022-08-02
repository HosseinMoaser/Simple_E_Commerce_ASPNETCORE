using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_E_Commerce.App.Models;
using Simple_E_Commerce.Data.Context;
using Simple_E_Commerce.Data.Models;
using System.Diagnostics;

namespace Simple_E_Commerce.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SimpleEcommerceDbContext _simpleEcommerceDbContext;
        private static Cart _cart = new Cart();
        
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

        public IActionResult AddToCart(int itemId)
        {
            var product = _simpleEcommerceDbContext.Products
                .Include(p => p.Item)
                .SingleOrDefault(p=> p.Id == itemId);

            if(product != null)
            {
                var cartItem = new CartItem()
                {
                    Item = product.Item,
                    Quantity = 1
                };
                _cart.AddItem(cartItem);
            }

            return RedirectToAction("ShowCart");
        }

        public IActionResult ShowCart()
        {
            var cartViewModel = new CartViewModel()
            {
                CartItems = _cart.CartItems,
                TotalCost = _cart.CartItems.Sum(c=> c.GetTotalPrice())
            };

            return View(cartViewModel);
        }

        public IActionResult RemoveCart(int itemId)
        {
            _cart.RemoveItem(itemId);
            return RedirectToAction("ShowCart");
        }

        public IActionResult Details(int id)
        {
            var product = _simpleEcommerceDbContext.Products
                .Include(p => p.Item)
                .SingleOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            var categories = _simpleEcommerceDbContext.Products
                .Where(p => p.Id == id)
                .SelectMany(c => c.CategoryToProducts)
                .Select(c => c.Category)
                .ToList();

            var viewModel = new DetailsViewModel()
            {
                Product = product,
                Categories = categories
            };
           
            return View(viewModel);
        }

        [Route("Group/{id}/{name}")]
        public IActionResult ShowProductsByGroup(int id , string name)
        {
            ViewBag.GroupName = name;
            var products = _simpleEcommerceDbContext.CategoryToProducts
                .Where(c => c.CategoryId == id)
                .Include(c => c.Product)
                .Select(c => c.Product)
                .ToList();
            return View(products);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}