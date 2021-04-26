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

        public async Task<UpdateBookViewModel> GetByIdAsync (int id)
        {
            return await _context.Books.Where(x => x.Id == id).Include(x => x.BookAuthors).ThenInclude(x => x.Author).Include(x => x.Category).Include(x => x.BookDeliveryTypes).Include(x => x.BookPaymentTypes).Select(x => new UpdateBookViewModel
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
                DeliveryTypesId = x.BookDeliveryTypes.Select(t => t.DeliveryTypeId).ToList(),
                PaymentTypesId = x.BookPaymentTypes.Select(m => m.PaymentTypeId).ToList(),
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
            var paymentTypeIds = await _context.BookPaymentTypes.Where(x => x.BookId == book.Id).Select(x => x.PaymentTypeId).ToListAsync();
            var prevPaymentTypeIds = new List<int>();
            var newBookPaymentTypes = new List<BookPaymentType>();
            foreach (var id in paymentTypeIds)
            {
                if (!model.PaymentTypesId.Contains(id))
                    prevPaymentTypeIds.Add(id);
            }
            foreach (var paymentTypeId in model.PaymentTypesId)
            {
                if(!paymentTypeIds.Contains(paymentTypeId))
                {
                    newBookPaymentTypes.Add(new BookPaymentType { BookId = book.Id, PaymentTypeId = paymentTypeId });
                }
            }
            var prevBookPaymentTypes = await _context.BookPaymentTypes.Where(x => x.BookId == book.Id && prevPaymentTypeIds.Contains(x.PaymentTypeId)).ToListAsync();
            _context.BookPaymentTypes.RemoveRange(prevBookPaymentTypes);
            await _context.BookPaymentTypes.AddRangeAsync(newBookPaymentTypes);

            var deliveryTypeIds = await _context.BookDeliveryTypes.Where(x => x.BookId == book.Id).Select(x => x.DeliveryTypeId).ToListAsync();
            var prevDeliveryTypeIds = new List<int>();
            var newBookDeliveryTypes = new List<BookDeliveryType>();
            foreach (var id in deliveryTypeIds)
            {
                if (!model.DeliveryTypesId.Contains(id))
                {
                    prevDeliveryTypeIds.Add(id);
                }
            }
            foreach (var deliveryTypeId in model.DeliveryTypesId)
            {
                if (!deliveryTypeIds.Contains(deliveryTypeId))
                {
                    newBookDeliveryTypes.Add(new BookDeliveryType { BookId = book.Id, DeliveryTypeId = deliveryTypeId });
                }
            }
            var prevBookDeliveTypes = await _context.BookDeliveryTypes.Where(x => x.BookId == book.Id && prevPaymentTypeIds.Contains(x.DeliveryTypeId)).ToListAsync();
            _context.BookDeliveryTypes.RemoveRange(prevBookDeliveTypes);
            await _context.BookDeliveryTypes.AddRangeAsync(newBookDeliveryTypes);

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
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
