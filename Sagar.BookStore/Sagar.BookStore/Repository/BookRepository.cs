using Sagar.BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sagar.BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }
        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel()
                {
                    Id=1,
                    Title="MVC",
                    Author="Sagar",
                    Description="This is MVC book",
                    Category="Programming",
                    Language="English",
                    TotalPages=1045
                },
                new BookModel()
                {
                    Id=2,
                    Title="Core",
                    Author="Sagar",
                    Description="This is Core book",
                    Category="Framework",
                    Language="English",
                    TotalPages=145
                },
                new BookModel()
                {
                    Id=3,
                    Title="C#",
                    Author="Chiku",
                    Description="This is C# book",
                    Category="Programming",
                    Language="English",
                    TotalPages=104
                },
                new BookModel()
                {
                    Id=4,
                    Title="Java",
                    Author="Yash",
                    Description="This is Java book",
                    Category="Programming",
                    Language="English",
                    TotalPages=1345
                },
                new BookModel()
                {
                    Id=5,
                    Title="React",
                    Author="Nayak",
                    Description="This is React book",
                    Category="Designing",
                    Language="English",
                    TotalPages=1045
                },
            };
        }
    }
}
