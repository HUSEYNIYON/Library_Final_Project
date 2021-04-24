using Library_Final_Project.Services.Book;
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

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAll();
            return View(books);
        }
    }
}
