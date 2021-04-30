using Library_Final_Project.Common.Enum;
using Library_Final_Project.DTOs;
using Library_Final_Project.Entities;
using Library_Final_Project.Services;
using Library_Final_Project.Services.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserService userService;
        private readonly OrderService orderService;
        private readonly BookService bookService;

        public OrderController(UserService userService, OrderService orderService, BookService bookService)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.bookService = bookService;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var currentUser = await userService.GetUserAsync(User);

            var order = await orderService.GetOrderBooksAsync(currentUser.Id);

            return View(order);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            var currentUser = await userService.GetUserAsync(User);

            await orderService.CreateAsync(model, currentUser.Id);

            return RedirectToAction("GetAll");
        }
    }
}
