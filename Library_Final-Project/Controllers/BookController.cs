using Library.Services;
using Library_Final_Project.DTOs.Book;
using Library_Final_Project.Services;
using Library_Final_Project.Services.Author;
using Library_Final_Project.Services.Book;
using Library_Final_Project.Services.DeliveryType;
using Library_Final_Project.Services.PaymentType;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public BookController(BookService bookService,
                              FileService fileService,
                              AuthorService authorService,
                              CategoryService categoryService,
                              DeliveryTypeService deliveryTypeService,
                              PaymentTypeService paymentTypeService)
        {
            _bookService = bookService;
            _fileService = fileService;
            _authorService = authorService;
            _categoryService = categoryService;
            _deliveryTypeService = deliveryTypeService;
            _paymentTypeService = paymentTypeService;
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
    }
}
