using Library_Final_Project.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Services.Cart
{
    public class CartBookService
    {
        private readonly LibraryDbContext _context;

        public CartBookService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<CartBook> FetchOneAsync(int bookId, string userId)
        {
            return await _context.CartBooks.FirstOrDefaultAsync(x => x.BookId == bookId && x.UserId == userId);
        }
    }
}
