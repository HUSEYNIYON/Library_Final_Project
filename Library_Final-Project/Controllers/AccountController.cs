using Library_Final_Project.DTOs;
using Library_Final_Project.DTOs.User;
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

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userService.SignUpAsync(model);

            if (result.Succeeded)
            {
                return RedirectToAction("SignIn");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await _userService.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
