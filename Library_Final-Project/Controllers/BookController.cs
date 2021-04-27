using Library.Services;
using Library_Final_Project.Common.Pagination;
using Library_Final_Project.DTOs.Book;
using Library_Final_Project.Services;
using Library_Final_Project.Services.Author;
using Library_Final_Project.Services.Book;
using Library_Final_Project.Services.DeliveryType;
using Library_Final_Project.Services.PaymentType;
using Microsoft.AspNetCore.Mvc;
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

        public BookController(BookService bookService,
                              FileService fileService,
                              AuthorService authorService,
                              CategoryService categoryService,
                              DeliveryTypeService deliveryTypeService,
                              PaymentTypeService paymentTypeService,
                              LibraryDbContext context)
        {
            _bookService = bookService;
            _fileService = fileService;
            _authorService = authorService;
            _categoryService = categoryService;
            _deliveryTypeService = deliveryTypeService;
            _paymentTypeService = paymentTypeService;
            _context = context;
        }
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            return View(await PaginatedList<Entities.Book>.CreateAsync(_context.Books, pageNumber, 8));
        }
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAll();
            return View(books);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var bookViewModel = new CreateBookViewModel()
            {
                Authors = await _authorService.GetAllInDictionaryAsync(),
                Categories = await _categoryService.GetAllInDictionaryAsync(),
                DeliveryTypes = await _deliveryTypeService.GetAllInDictionaryAsync(),
                PaymentTypes = await _paymentTypeService.GetAllInDictionaryAsync()
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
        public async Task<IActionResult> Update(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            book.Authors = await _authorService.GetAllInDictionaryAsync();
            book.PaymentTypes = await _paymentTypeService.GetAllInDictionaryAsync();
            book.DeliveryTypes = await _deliveryTypeService.GetAllInDictionaryAsync();
            book.Categories = await _categoryService.GetAllInDictionaryAsync();
            return View(book);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBookViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Authors = await _authorService.GetAllInDictionaryAsync();
                model.DeliveryTypes = await _deliveryTypeService.GetAllInDictionaryAsync();
                model.PaymentTypes = await _paymentTypeService.GetAllInDictionaryAsync();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.Delete(id);
            return RedirectToAction("GetBooks");
        }
    }
}
