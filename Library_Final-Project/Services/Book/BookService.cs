using Library_Final_Project.DTOs.Book;
using Library_Final_Project.Entities;
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
            return await _context.Books.Select(x => new BookViewModel
            {
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
            var bookPaymentTypes = new List<BookPaymentType>();
            foreach (var id in model.PaymentTypesId)
            {
                bookPaymentTypes.Add(new BookPaymentType { BookId = book.Id, PaymentTypeId = id });
            }
            var bookDeliveryTypes = new List<BookDeliveryType>();
            foreach (var id in model.DeliveryTypesId)
            {
                bookDeliveryTypes.Add(new BookDeliveryType { BookId = book.Id, DeliveryTypeId = id });
            }
            var bookAuthors = new List<BookAuthor>();
            foreach (var id in model.PaymentTypesId)
            {
                bookAuthors.Add(new BookAuthor { BookId = book.Id, AuthorId = id });
            }
            await _context.BookPaymentTypes.AddRangeAsync(bookPaymentTypes);
            await _context.BookDeliveryTypes.AddRangeAsync(bookDeliveryTypes);
            await _context.BookAuthors.AddRangeAsync(bookAuthors);
            await _context.SaveChangesAsync();
        }
    }
}
