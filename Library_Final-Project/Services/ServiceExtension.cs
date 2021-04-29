using Library.Services;
using Library_Final_Project.Services.Author;
using Library_Final_Project.Services.Book;
using Library_Final_Project.Services.Cart;
using Library_Final_Project.Services.DeliveryType;
using Library_Final_Project.Services.PaymentType;
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
            services.AddTransient<BookService>();
            services.AddTransient<FileService>();
            services.AddTransient<PaymentTypeService>();
            services.AddTransient<DeliveryTypeService>();
            services.AddTransient<CartBookService>();
        }
    }
}
