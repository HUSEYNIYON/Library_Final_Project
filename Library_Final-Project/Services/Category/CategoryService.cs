using Library_Final_Project.Context;
using Library_Final_Project.DTOs.Category;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Services.Category
{
    public class CategoryService
    {
        private readonly LibraryDbContext _context;

        public CategoryService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryViewModel>> GetAll()
        {
            return await _context.Categories.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                IconPath = x.IconPath
            }).ToListAsync();
        }
        public async Task CreateAsync(CreateCategoryViewModel model, string iconPath)
        {
            var category = new Library_Final_Project.Entities.Category
            {
                Name = model.Name,
                ParentCategoryId = model.ParentCategoryId,
                IconPath = iconPath
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
    }
}
