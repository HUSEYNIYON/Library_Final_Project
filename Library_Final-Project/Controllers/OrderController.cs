using Library_Final_Project.Common.Enum;
using Library_Final_Project.DTOs;
using Library_Final_Project.Services;
using Library_Final_Project.Services.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Final_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserService _userService;
        private readonly OrderService _orderService;
        private readonly BookService _bookService;

        public OrderController(UserService userService, OrderService orderService, BookService bookService)
        {
            _userService = userService;
            _orderService = orderService;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAll();
            return View(orders);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userService.GetUserAsync(User);

            var order = await _orderService.GetOrderBooksAsync(currentUser.Id);

            return View(order);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            var currentUser = await _userService.GetUserAsync(User);

            await _orderService.CreateAsync(model, currentUser.Id);

            return RedirectToAction("Index","Home");
        }
    }
}
