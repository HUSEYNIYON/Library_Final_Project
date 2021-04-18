using Library_Final_Project.Services.Author;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Final_Project.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorService.GetAll();
            return View(authors);
        }
    }
}
