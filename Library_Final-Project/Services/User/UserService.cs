using Library_Final_Project.DTOs;
using Library_Final_Project.DTOs.User;
using Library_Final_Project.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library_Final_Project.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await _userManager.GetUserAsync(claimsPrincipal);
        }

        public async Task<SignInResult> SignInAsync(SignInViewModel viewModel)
        {
            return await _signInManager.PasswordSignInAsync(viewModel.Login, viewModel.Password, false, false);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpViewModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Login
            };

            var response = await _userManager.CreateAsync(user, model.Password);

            if (response.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return response;
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
