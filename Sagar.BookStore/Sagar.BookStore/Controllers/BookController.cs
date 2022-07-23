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
            return View();
        } 
        public BookModel GetBook(int id)
        {
            return _bookRepository.GetBookById(id);
        } 
        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName,authorName);
        }
    }
}
