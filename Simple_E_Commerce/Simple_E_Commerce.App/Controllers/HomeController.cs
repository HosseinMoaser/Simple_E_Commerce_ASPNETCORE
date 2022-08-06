using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_E_Commerce.App.Models;
using Simple_E_Commerce.Data.Context;
using Simple_E_Commerce.Data.Models;
using System.Diagnostics;
using System.Security.Claims;

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

        [Authorize]
        public IActionResult AddToCart(int itemId)
        {
            var product = _simpleEcommerceDbContext.Products
                .Include(p => p.Item)
                .SingleOrDefault(p => p.Id == itemId);

            if (product != null)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var order = _simpleEcommerceDbContext.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinally);
                if (order != null)
                {
                    var orderDetail = _simpleEcommerceDbContext.OrderDetails.FirstOrDefault(d => d.OrderId == order.OrderId
                    && d.ProductId == product.Id);
                    if (orderDetail != null)
                        orderDetail.Quantity += 1;
                    else
                        _simpleEcommerceDbContext.OrderDetails.Add(new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            ProductId = product.Id,
                            Price = product.Item.Price,
                            Quantity = 1
                        });
                }
                else
                {
                    order = new Order()
                    {
                        IsFinally = false,
                        UserId = userId,
                        CreateDate = DateTime.Now,
                    };
                    _simpleEcommerceDbContext.Orders.Add(order);
                    _simpleEcommerceDbContext.SaveChanges();
                    _simpleEcommerceDbContext.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = product.Id,
                        Price = product.Item.Price,
                        Quantity = 1
                    });
                }
                _simpleEcommerceDbContext.SaveChanges();
            }

            return RedirectToAction("ShowCart");
        }

        [Authorize]
        public IActionResult ShowCart()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _simpleEcommerceDbContext.Orders.Where(o => o.UserId == userId && !o.IsFinally)
                .Include(o => o.OrderDetails)
                .ThenInclude(c => c.Product).FirstOrDefault();

            return View(order);
        }

        [Authorize]
        public IActionResult RemoveCart(int detailId)
        {
            var orderDetail = _simpleEcommerceDbContext.OrderDetails.Find(detailId);
            _simpleEcommerceDbContext.Remove(orderDetail);
            _simpleEcommerceDbContext.SaveChanges();
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
        public IActionResult ShowProductsByGroup(int id, string name)
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