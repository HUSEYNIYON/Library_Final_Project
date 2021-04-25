using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Library_Final_Project.DTOs.Category;
using Library_Final_Project.Entities;

namespace Library.Services
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
            var category = new Category
            {
                Name = model.Name,
                ParentCategoryId = model.ParentCategoryId,
                IconPath = iconPath
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public void FindChildCategories(List<Category> categories, int? id, ref List<int> catIds)
        {
            foreach (var category in categories)
            {
                if (category.ParentCategoryId == id)
                {
                    catIds.Add(category.Id);
                    var childCategories = categories.Where(x => x.ParentCategoryId == category.Id).ToList();
                    FindChildId(childCategories, ref catIds);
                }
            }
        }

        public void FindChildId(List<Category> categories, ref List<int> catIds)
        {
            foreach (var category in categories)
            {
                catIds.Add(category.Id);
                var childCategories = categories.Where(x => x.ParentCategoryId == category.Id).ToList();
                FindChildId(childCategories, ref catIds);
            }
        }

        public async Task<Dictionary<int, string>> GetAllInDictionaryAsync(int? id = null)
        {
            var categories = await _context.Categories.ToListAsync();

            List<int> catIds = new List<int>();

            if (id != null)
            {
                catIds.Add(id ?? 0);
                FindChildCategories(categories, id, ref catIds);
            }

            categories = categories.Where(x => !catIds.Contains(x.Id)).ToList();

            return categories.ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<bool> EditAsync(EditCategoryViewModel model, string filePath)
        {
            var category = await _context.Categories.FindAsync(model.Id);

            if (category == null)
            {
                return false;
            }
            category.Name = model.Name;
            category.IconPath = filePath;
            category.ParentCategoryId = model.ParentCategoryId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}