using Library_Final_Project.DTOs;
using Library_Final_Project.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userService.SignInAsync(model);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "/Home/Index");
            }
            ModelState.AddModelError("SignIn", "Логин или пароль неверный");
            return View(model);
        }
    }
}
