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
            var admin = await userManager.FindByNameAsync("admin");
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin"
                };
                await userManager.CreateAsync(admin, "admin99");
                await userManager.AddToRoleAsync(admin, nameof(Roles.Admin));
            }

            var user = await userManager.FindByNameAsync("user");
            if(user == null)
            {
                user = new ApplicationUser
                {
                    Email = "user@gmail.com",
                    UserName = "user"
                };
                await userManager.CreateAsync(user, "user99");
                await userManager.AddToRoleAsync(admin, nameof(Roles.User));
            }
        }
    }
}
