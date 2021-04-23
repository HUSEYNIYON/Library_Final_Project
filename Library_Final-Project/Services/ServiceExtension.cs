using Library_Final_Project.Services.Author;
using Library_Final_Project.Services.Category;
using Library_Final_Project.Services.Discount;
using Microsoft.Extensions.DependencyInjection;

namespace Library_Final_Project.Services
{
    public static class ServiceExtension
    {
        public static void InitServices(this IServiceCollection services)
        {
            services.AddTransient<UserService>();
            services.AddTransient<AuthorService>();
            services.AddTransient<CategoryService>();
            services.AddTransient<DiscountService>();
        }
    }
}
