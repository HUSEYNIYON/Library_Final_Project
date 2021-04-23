using Library_Final_Project.DTOs.Discount;
using Library_Final_Project.Services.Discount;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Controllers
{
    public class DiscountController : Controller
    {
        private readonly DiscountService _discountService;

        public DiscountController(DiscountService discountService)
        {
            _discountService = discountService;
        }
        public async Task<IActionResult> GetDiscounts()
        {
            var discounts = await _discountService.GetAll();
            return View(discounts);
        }
        [HttpGet]   
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDiscountViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            await _discountService.CreateAsync(model);
            return RedirectToAction("GetDiscounts");
        }
    }
}
