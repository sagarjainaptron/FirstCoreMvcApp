using Microsoft.EntityFrameworkCore;
using Sagar.BookStore.Data;
using Sagar.BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sagar.BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };
            newBook.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl
                }).ToListAsync();
        }
        public async Task<List<BookModel>> GetTopBooksAsync()
        {
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl
                }).Take(5).ToListAsync();
        }
        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallery = book.bookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl
                }).FirstOrDefaultAsync();
        }
        //return DataSource().Where(x => x.Id == id).FirstOrDefault();
        public List<BookModel> SearchBook(string title, string authorName)
        {
            //return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
            return null;
        }
        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel()
        //        {
        //            Id=1,
        //            Title="MVC",
        //            Author="Sagar",
        //            Description="This is MVC book",
        //            Category="Programming",
        //            Language="English",
        //            TotalPages=1045
        //        },
        //        new BookModel()
        //        {
        //            Id=2,
        //            Title="Core",
        //            Author="Sagar",
        //            Description="This is Core book",
        //            Category="Framework",
        //            Language="English",
        //            TotalPages=145
        //        },
        //        new BookModel()
        //        {
        //            Id=3,
        //            Title="C#",
        //            Author="Chiku",
        //            Description="This is C# book",
        //            Category="Programming",
        //            Language="English",
        //            TotalPages=104
        //        },
        //        new BookModel()
        //        {
        //            Id=4,
        //            Title="Java",
        //            Author="Yash",
        //            Description="This is Java book",
        //            Category="Programming",
        //            Language="English",
        //            TotalPages=1345
        //        },
        //        new BookModel()
        //        {
        //            Id=5,
        //            Title="React",
        //            Author="Nayak",
        //            Description="This is React book",
        //            Category="Designing",
        //            Language="English",
        //            TotalPages=1045
        //        },
        //    };
        //}
    }
}
