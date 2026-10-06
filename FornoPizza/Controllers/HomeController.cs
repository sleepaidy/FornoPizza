using System.Diagnostics;
using FornoPizza.Data.Repository.Interfaces;
using FornoPizza.Models;
using FornoPizza.Models.Home;
using FornoPizza.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FornoPizza.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;
        private readonly IMenuRepository _menuRepository;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService, IMenuRepository menuRepository)
        {
            _logger = logger;
            _orderService = orderService;
            _menuRepository = menuRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Pizzas = _menuRepository.GetPizzaMenu(),
                Toppings = _menuRepository.GetToppingsMenu()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderViewModel viewModel)
        {
            var orderId = _orderService.CreateOrder(viewModel);
            return RedirectToAction(nameof(OrderSuccess), new { id = orderId });
        }

        [HttpGet]
        public IActionResult OrderSuccess(int id)
        {
            return View(id);
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

        [Authorize]
        [HttpGet]
        public IActionResult MyOrders()
        {
            var orders = _orderService.GetMyOrders();
            return View(orders);
        }

    }
}
