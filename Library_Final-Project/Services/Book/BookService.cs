using Library_Final_Project.Common;
using Library_Final_Project.Common.Pagination;
using Library_Final_Project.DTOs.Book;
using Library_Final_Project.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
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
            return await _context.Books.Select(x => new BookViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ImagePath = x.ImagePath,
                Description = x.Description,
                Percent = x.Percent,
                Price = x.Price
            }).ToListAsync();
        }
        public async Task CreateAsync(CreateBookViewModel model, string imagePath, string pdfPath)
        {
            var book = new Entities.Book
            {
                Available = model.Available,
                Description = model.Description,
                Language = model.Language,
                Percent = model.Percent,
                Price = model.Price,
                Title = model.Title,
                AvailableCount = model.AvailableCount,
                CategoryId = model.CategoryId,
                HasPdf = pdfPath != null,
                PdfPath = pdfPath,
                ImagePath = imagePath,
                PagesNumber = model.PagesNumber,
                PublishYear = model.PublishYear,
                ISBN = model.ISBN
            };
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            var bookAuthors = new List<BookAuthor>();
            foreach (var id in model.AuthorsId)
            {
                bookAuthors.Add(new BookAuthor { BookId = book.Id, AuthorId = id });
            }
            await _context.BookAuthors.AddRangeAsync(bookAuthors);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<BookViewModel>> GetPagedBookAsync(int pageIndex, int pageSize)
        {
            var booksCount = await _context.Books.CountAsync();
            var items = await _context.Books.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(x => x.BookAuthors).ThenInclude(x => x.Author).Select(x => new BookViewModel
            {
                Id = x.Id,
                Description = x.Description,
                Percent = x.Percent,
                Price = x.Price,
                Title = x.Title,
                ImagePath = x.ImagePath,
                Author = x.BookAuthors.FirstOrDefault().Author.Name
            }).ToListAsync();
            return new PaginatedList<BookViewModel>(items, booksCount, pageIndex, pageSize);
        }

        public async Task<UpdateBookViewModel> GetByIdAsync (int id)
        {
            return await _context.Books.Where(x => x.Id == id).Include(x => x.BookAuthors).ThenInclude(x => x.Author).Include(x => x.Category).Select(x => new UpdateBookViewModel
            {
                AuthorsId = x.BookAuthors.Select(p => p.AuthorId).ToList(),
                Available = x.Available,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Id = x.Id,
                PrevImagePath = x.ImagePath,
                Language = x.Language,
                Percent = x.Percent,
                Price = x.Price,
                Title = x.Title,
                AvailableCount = x.AvailableCount,
                PagesNumber = x.PagesNumber,
                ISBN = x.ISBN,
                PublishYear = x.PublishYear,
                PrevPdfPath = x.PdfPath
            }).FirstOrDefaultAsync();
        }
        public async Task UpdateAsync(UpdateBookViewModel model, string imagePath, string pdfPath)
        {
            var book = await _context.Books.FindAsync(model.Id);
            if(book == null)
            {
                return;
            }

            book.Available = model.Available;
            book.CategoryId = model.CategoryId;
            book.Description = model.Description;
            book.Language = model.Language;
            book.Percent = model.Percent;
            book.Price = model.Price;
            book.Title = model.Title;
            book.AvailableCount = model.AvailableCount;
            book.HasPdf = pdfPath != null;
            book.PdfPath = pdfPath;
            book.ISBN = model.ISBN;
            book.ImagePath = imagePath;
            book.PagesNumber = model.PagesNumber;
            book.PublishYear = model.PublishYear;
            var authorsId = await _context.BookAuthors.Where(x => x.BookId == book.Id).Select(x => x.AuthorId).ToListAsync();
            var prevAuthorsId = new List<int>();
            var newBookAuthors = new List<BookAuthor>();
            foreach (var id in authorsId)
            {
                if (!model.AuthorsId.Contains(id))
                    prevAuthorsId.Add(id);
            }
            foreach (var authorId in model.AuthorsId)
            {
                if (!authorsId.Contains(authorId))
                    newBookAuthors.Add(new BookAuthor { BookId = book.Id, AuthorId = authorId });
            }
            var prevBookAuthors = await _context.BookAuthors.Where(x => x.BookId == book.Id && prevAuthorsId.Contains(x.AuthorId)).ToListAsync();
            _context.BookAuthors.RemoveRange(prevBookAuthors);
            await _context.BookAuthors.AddRangeAsync(newBookAuthors);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book == null)
            {
                return;
            }
            var authors = await _context.BookAuthors.Where(x => x.BookId == book.Id).ToListAsync();
            _context.BookAuthors.RemoveRange(authors);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Response> AddToCartAsync(int bookId, string userId)
        {
            var cartBook = await _context.CartBooks.FirstOrDefaultAsync(x => x.BookId == bookId && x.UserId == userId);
            if(cartBook != null)
            {
                cartBook.Count++;
            }
            else
            {
                cartBook = new CartBook
                {
                    Count = 1,
                    BookId = bookId,
                    UserId = userId
                };
                await _context.CartBooks.AddAsync(cartBook);
            }
            var result = await _context.SaveChangesAsync();
            return new Response { Succeeded = result > 0, Message = result > 0 ? null : "Ошибка при добавлении" };
        }
        public async Task<Response> DeleteFromCartAsync(int bookId, string userId)
        {
            var cartBook = await _context.CartBooks.FirstOrDefaultAsync(x => x.BookId == bookId && x.UserId == userId);
            if(cartBook.Count > 1)
            {
                cartBook.Count--;
            }
            else
            {
                _context.CartBooks.Remove(cartBook);
            }
            var result = await _context.SaveChangesAsync();
            return new Response { Succeeded = result > 0, Message = result > 0 ? null : "Ошибка при добавлении" };
        }
        public async Task<List<CartBookViewModel>> GetCartBooksAsync(string currentUserId)
        {
            return await _context.CartBooks.Include(x => x.Book).Select(x => new CartBookViewModel
            {
                Count = x.Count,
                Name = x.Book.Title,
                Price = x.Book.Price,
                BookId = x.BookId,
                ImagePath = x.Book.ImagePath
            }).ToListAsync();
        }
        public async Task<Response> DeleteAllFromCartAsync(int bookId, string currentUserId)
        {
            var cartBook = await _context.CartBooks.FirstOrDefaultAsync(x => x.BookId == bookId && x.UserId == currentUserId);
            _context.CartBooks.Remove(cartBook);
            var result = await _context.SaveChangesAsync();
            return new Response { Succeeded = result > 0, Message = result > 0 ? null : "Ошибка при удалении" };
        }

    }
}
