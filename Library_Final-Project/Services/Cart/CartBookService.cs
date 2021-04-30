using Library_Final_Project.Entities;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// FetchOneAsync
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<CartBook> FetchOneAsync(int bookId, string userId)
        {
            return await _context.CartBooks.FirstOrDefaultAsync(x => x.BookId == bookId && x.UserId == userId);
        }
    }
}
