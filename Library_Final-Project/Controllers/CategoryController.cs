using Library.Services;
using Library_Final_Project.Common.Enum;
using Library_Final_Project.DTOs.Category;
using Library_Final_Project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library_Final_Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly FileService _fileService;

        public CategoryController(CategoryService categoryService, FileService fileService)
        {
            _categoryService = categoryService;
            _fileService = fileService;
        }

        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin)+ " " + nameof(Roles.User))]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAll();
            return View(categories);
        }

        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> Create()
        {
            return View(new CreateCategoryViewModel
            {
                Categories = await _categoryService.GetAllInDictionaryAsync()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create (CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryService.GetAllInDictionaryAsync();
                return View(model);
            }

            string filePath = null;

            if (model.File != null)
            {
                _fileService.CreateDirectory();
                filePath = await _fileService.AddFileAsync(model.File);
            }

            await _categoryService.CreateAsync(model, filePath);

            return RedirectToAction("GetCategories");
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            return View(new EditCategoryViewModel
            {
                Id = category.Id,
                Categories = await _categoryService.GetAllInDictionaryAsync(id),
                Name = category.Name,
                IconPath = category.IconPath,
                ParentCategoryId = category.ParentCategoryId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryService.GetAllInDictionaryAsync(model.Id);
                return View(model);
            }

            string filePath = model.IconPath;

            if (model.File != null)
            {
                _fileService.CreateDirectory();
                _fileService.DeleteFile(model.IconPath);
                filePath = await _fileService.AddFileAsync(model.File);
            }

            var result = await _categoryService.EditAsync(model, filePath);

            return result ? RedirectToAction("GetCategories") : NotFound();
        }
    }
}
