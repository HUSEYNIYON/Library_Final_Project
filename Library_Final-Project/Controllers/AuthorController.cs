using Library_Final_Project.Common.Enum;
using Library_Final_Project.DTOs.Author;
using Library_Final_Project.Services.Author;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> Create(CreateAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _authorService.CreateAsync(model);
            return RedirectToAction("GetAuthors");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.Delete(id);
            return RedirectToAction(nameof(GetAuthors));
        }
    }
}
