using Library_Final_Project.DTOs;
using Library_Final_Project.Entities;
using Microsoft.AspNetCore.Identity;
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

        public async Task<SignInResult> SignInAsync(SignInViewModel viewModel)
        {
            return await _signInManager.PasswordSignInAsync(viewModel.Login, viewModel.Password, false, false);
        }
    }
}
