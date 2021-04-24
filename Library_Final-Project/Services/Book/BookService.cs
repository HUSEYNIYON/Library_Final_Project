using Library_Final_Project.DTOs.Book;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Services.Book
{
    public class BookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<List<BookViewModel>> GetAll()
        {
            var books = await _context.Books.Select(x => new BookViewModel
            {
                Title = x.Title,
                ImagePath = x.ImagePath,
                Description = x.Description,
                Percent = x.Percent,
                Price = x.Price
            }).ToListAsync();
            return books;
        }
    }
}
