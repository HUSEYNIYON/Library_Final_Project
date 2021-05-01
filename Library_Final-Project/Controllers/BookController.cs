using Library.Services;
using Library_Final_Project.Common.Enum;
using Library_Final_Project.DTOs.Book;
using Library_Final_Project.Services;
using Library_Final_Project.Services.Author;
using Library_Final_Project.Services.Book;
using Library_Final_Project.Services.Cart;
using Library_Final_Project.Services.DeliveryType;
using Library_Final_Project.Services.PaymentType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly FileService _fileService;
        private readonly AuthorService _authorService;
        private readonly CategoryService _categoryService;
        private readonly DeliveryTypeService _deliveryTypeService;
        private readonly PaymentTypeService _paymentTypeService;
        private readonly LibraryDbContext _context;
        private readonly CartBookService _cartBookService;
        private readonly UserService _userService;

        public BookController(BookService bookService,
                              FileService fileService,
                              AuthorService authorService,
                              CategoryService categoryService,
                              DeliveryTypeService deliveryTypeService,
                              PaymentTypeService paymentTypeService,
                              LibraryDbContext context,
                              CartBookService cartBookService,
                              UserService userService)
        {
            _bookService = bookService;
            _fileService = fileService;
            _authorService = authorService;
            _categoryService = categoryService;
            _deliveryTypeService = deliveryTypeService;
            _paymentTypeService = paymentTypeService;
            _context = context;
            _cartBookService = cartBookService;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var books = await _bookService.GetPagedBookAsync(pageNumber, 10);
            return View(books);
        }

        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAll();
            return View(books);
        }

        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> Create()
        {
            var bookViewModel = new CreateBookViewModel()
            {
                Authors = await _authorService.GetAllInDictionaryAsync(),
                Categories = await _categoryService.GetAllInDictionaryAsync()
            };
            return View(bookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            string pdfPath = null;
            string imagePath = null;
            _fileService.CreateDirectory();
            if(model.PdfFile != null)
            {
                pdfPath = await _fileService.AddFileAsync(model.PdfFile);
            }
            if(model.Image != null)
            {
                imagePath = await _fileService.AddFileAsync(model.Image);   
            }
            await _bookService.CreateAsync(model, imagePath, pdfPath);
            return RedirectToAction("GetBooks");
        }
        
        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> Update(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            book.Authors = await _authorService.GetAllInDictionaryAsync();
            book.Categories = await _categoryService.GetAllInDictionaryAsync();
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBookViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Authors = await _authorService.GetAllInDictionaryAsync();
                model.Categories = await _categoryService.GetAllInDictionaryAsync();
                return View();
            }
            string imagePath = model.PrevImagePath;
            if(model.Image != null)
            {
                _fileService.DeleteFile(imagePath);
                imagePath = await _fileService.AddFileAsync(model.Image);
            }
            string pdfPath = model.PrevPdfPath;
            if(model.PdfFile != null)
            {
                _fileService.DeleteFile(pdfPath);
                pdfPath = await _fileService.AddFileAsync(model.PdfFile);
            }
            await _bookService.UpdateAsync(model, imagePath, pdfPath);
            return RedirectToAction("GetBooks");
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.Delete(id);
            return RedirectToAction("GetBooks");
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            var currentUser = await _userService.GetUserAsync(User);
            await _bookService.AddToCartAsync(bookId, currentUser.Id);
            return RedirectToAction("CartBooks");
        }

        [Authorize]
        public async Task<IActionResult> DeleteFromCart(int bookId)
        {
            var currentUser = await _userService.GetUserAsync(User);
            await _bookService.DeleteFromCartAsync(bookId, currentUser.Id);
            return RedirectToAction("CartBooks");
        }

        [Authorize]
        public async Task<IActionResult> DeleteAllFromCart(int bookId)
        {
            var currentUser = await _userService.GetUserAsync(User);
            await _bookService.DeleteAllFromCartAsync(bookId, currentUser.Id);
            return RedirectToAction("CartBooks");
        }

        [Authorize]
        public async Task<IActionResult> CartBooks()
        {
            var currentUser = await _userService.GetUserAsync(User);
            var cartBooks = await _bookService.GetCartBooksAsync(currentUser.Id);
            return View(cartBooks);
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var books = from m in _context.Books
                select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }

            return View(await books.ToListAsync());
        }
    }
}
