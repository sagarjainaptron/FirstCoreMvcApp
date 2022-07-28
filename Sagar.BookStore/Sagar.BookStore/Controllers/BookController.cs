using Microsoft.AspNetCore.Mvc;
using Sagar.BookStore.Models;
using Sagar.BookStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sagar.BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        public BookController()
        {
            _bookRepository = new BookRepository();
        }
        public IActionResult GetAllBooks()
        {
            var data =  _bookRepository.GetAllBooks();
            return View(data);
        } 
        public IActionResult GetBook(int id)
        {
            var data= _bookRepository.GetBookById(id);
            return View(data);
        } 
        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName,authorName);
        }
        public IActionResult AddNewBook()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewBook(BookModel bookModel)
        {
            return View();
        }
    }
}
