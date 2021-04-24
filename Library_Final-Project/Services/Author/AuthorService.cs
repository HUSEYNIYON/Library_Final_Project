using Library_Final_Project.DTOs;
using Library_Final_Project.DTOs.Author;
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
        public async Task CreateAsync(CreateAuthorViewModel model)
        {
            var author = new Entities.Author
            {
                Name = model.Name
            };
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }
    }
}
