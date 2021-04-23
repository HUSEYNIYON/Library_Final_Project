using Library_Final_Project.Services.Category;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Final_Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAll();
            return View(categories);
        }
    }
}
