using Library_Final_Project.Common.Enum;    
using Library_Final_Project.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Library_Final_Project.Seeds
{
    public class DefualtUsersSeed
    {
        public static async Task AddDefaultUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var admin = await userManager.FindByNameAsync("Admin");
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    Email = "Admin@mail.ru",
                    UserName = "Admin"
                };
                await userManager.CreateAsync(admin, "@dmin123");
                await userManager.AddToRoleAsync(admin, nameof(Roles.Admin));
            }

            var user = await userManager.FindByNameAsync("User");
            if(user == null)
            {
                user = new ApplicationUser
                {
                    Email = "User@mail.ru",
                    UserName = "User"
                };
                await userManager.CreateAsync(user, "@user123");
                await userManager.AddToRoleAsync(admin, nameof(Roles.User));
            }
        }
    }
}
