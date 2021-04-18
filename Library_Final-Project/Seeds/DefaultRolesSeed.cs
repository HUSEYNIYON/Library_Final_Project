using Library_Final_Project.Common.Enum;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Library_Final_Project.Seeds
{
    public class DefaultRolesSeed
    {
        public static async Task AddDefaultRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync(nameof(Roles.Admin)))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = nameof(Roles.Admin)});
            }
            if(!await roleManager.RoleExistsAsync(nameof(Roles.User)))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = nameof(Roles.User) });
            }
        }
    }
}
