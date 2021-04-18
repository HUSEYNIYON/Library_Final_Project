using Library_Final_Project.Context;
using Library_Final_Project.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Services.Author
{
    public class AuthorService
    {
        private readonly LibraryDbContext _context;

        public AuthorService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<List<AuthorViewModel>> GetAll()
        {
            var authors = await _context.Authors.Select(x => new AuthorViewModel{
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return authors;
        }
    }
}
