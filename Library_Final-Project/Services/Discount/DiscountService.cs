using Library_Final_Project.Context;
using Library_Final_Project.DTOs.Discount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Services.Discount
{
    public class DiscountService
    {
        private readonly LibraryDbContext _context;

        public DiscountService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<List<CreateDiscountViewModel>> GetAll()
        {
            var discounts = await _context.Discounts.Select(x => new CreateDiscountViewModel
            {
                Price = x.Price,
                Percent = x.Percent
            }).ToListAsync();
            return discounts;
        }
        public async Task CreateAsync(CreateDiscountViewModel model)
        {
            var discounts = new Entities.Discount
            {
                Price = model.Price,
                Percent = model.Percent
            };
            await _context.Discounts.AddAsync(discounts);
            await _context.SaveChangesAsync();
        }
    }
}
