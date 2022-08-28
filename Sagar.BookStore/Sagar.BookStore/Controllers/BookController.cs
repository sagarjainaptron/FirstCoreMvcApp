using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sagar.BookStore.Models;
using Sagar.BookStore.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sagar.BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;

        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        } 
        public async Task<IActionResult> GetBook(int id)
        {
            var data= await _bookRepository.GetBookById(id);
            return View(data);
        } 
        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName,authorName);
        }
        public async Task<IActionResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            // for selected default option value in dropdown
            var model = new BookModel()
            {
               // LanguageId = 3
            };
            // ViewBag.Language = new SelectList(GetLanguage(),"Id","Text"); -----------method 1

            //ViewBag.Language = GetLanguage().Select(x => new SelectListItem() --------------method 2
            //{
            //    Text = x.Text,
            //    Value=x.Id.ToString()
            //}).ToList();

            //var group1 = new SelectListGroup()---------------to grop items in drop down
            //{
            //    Name = "Group1"
            //};
            //var group2 = new SelectListGroup()
            //{
            //    Name = "Group2",
            //    Disabled=true
            //};
            //var group3 = new SelectListGroup()
            //{
            //    Name = "Group3"
            //};

            //ViewBag.Language = new List<SelectListItem>()--------------------method 3
            //{
            //    new SelectListItem()
            //    {
            //        Text="Hindi",
            //        Value="1",
            //       // Group=group1
            //    },
            //    new SelectListItem()
            //    {
            //        Text="English",
            //        Value="2",
            //       // Disabled=true
            //       //Group=group1
            //    },
            //    new SelectListItem()
            //    {
            //        Text="Dutch",
            //        Value="3",
            //        //Selected=true , //selected for default value for dropdown
            //      //  Group=group2
            //    },
            //    new SelectListItem()
            //    {
            //        Text="Tamil",
            //        Value="4",
            //       // Group=group2
            //    },
            //    new SelectListItem()
            //    {
            //        Text="Urdu",
            //        Value="5",
            //       // Group=group3
            //    },
            //    new SelectListItem()
            //    {
            //        Text="Chinese",
            //        Value="6",
            //       // Group=group3
            //    },
            //};

             ViewBag.Language = new SelectList(await _languageRepository.GetLanguage(), "Id","Name");

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                
                if(bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl= await UploadImage(folder, bookModel.CoverPhoto);
                }
                
                if(bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";
                    bookModel.Gallery = new List<GalleryModel>();
                    foreach(var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                    bookModel.Gallery.Add(gallery);
                       
                    }             
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }
            // ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");------------method 1

            //var group1 = new SelectListGroup()
            //{
            //    Name = "Group1"
            //};
            //var group2 = new SelectListGroup()
            //{
            //    Name = "Group2",
            //    Disabled = true
            //};
            //var group3 = new SelectListGroup()
            //{
            //    Name = "Group3"
            //};

            //ViewBag.Language = new List<SelectListItem>()---------------method 3
            //{
            //    new SelectListItem()
            //    {
            //        Text="Hindi",
            //        Value="1",
            //        //Group=group1
            //    },
            //    new SelectListItem()
            //    {
            //        Text="English",
            //        Value="2",
            //       // Disabled=true
            //      // Group=group1
            //    },
            //    new SelectListItem()
            //    {
            //        Text="Dutch",
            //        Value="3",
            //        //Selected=true , //selected for default value for dropdown
            //        //Group=group2
            //    },
            //    new SelectListItem()
            //    {
            //        Text="Tamil",
            //        Value="4",
            //       // Group=group2
            //    },
            //    new SelectListItem()
            //    {
            //        Text="Urdu",
            //        Value="5",
            //       // Group=group3
            //    },
            //    new SelectListItem()
            //    {
            //        Text="Chinese",
            //        Value="6",
            //       // Group=group3
            //    },
            //};

            ViewBag.Language = new SelectList(await _languageRepository.GetLanguage(), "Id", "Name");

            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }

        //private List<LanguageModel> GetLanguage()
        //{
        //    return new List<LanguageModel>()
        //    {
        //        new LanguageModel(){Id=1, Text="Hindi"},
        //        new LanguageModel(){Id=2, Text="English"},
        //        new LanguageModel(){Id=3, Text="Dutch"},
        //    };
        //}
    }
}
