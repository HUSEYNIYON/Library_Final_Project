using Library_Final_Project.Common;
using Library_Final_Project.DTOs;
using Library_Final_Project.DTOs.User;
using Library_Final_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Library_Final_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl)
        {
            return View(new SignInViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<JsonResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(JsonConvert.SerializeObject(new Response { Succeeded = false, Message = "Неправильно заполнены поля" }));
            }

            var result = await _userService.SignInAsync(model);

            if (result.Succeeded)
            {
                return Json(JsonConvert.SerializeObject(new Response { Succeeded = true }));
            }

            return Json(JsonConvert.SerializeObject(new Response
            { Message = "Логин или пароль неверны", Succeeded = false }));
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(JsonConvert.SerializeObject(new Response { Succeeded = false, Message = "Неправильно заполнены поля" }));
            }

            var result = await _userService.SignUpAsync(model);

            if (result.Succeeded)
            {
                return Json(JsonConvert.SerializeObject(new Response { Succeeded = true }));
            }

            string message = "q";

            foreach (var error in result.Errors)
            {
                message += $"{error.Description}\n";
            }

            return Json(JsonConvert.SerializeObject(new Response { Succeeded = false, Message = message }));
        }

        public async Task<IActionResult> SignOut()
        {
            await _userService.SignOutAsync();

            return RedirectToAction("index","Home");
        }
    }
}