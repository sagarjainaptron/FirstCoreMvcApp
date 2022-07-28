using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sagar.BookStore.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]
        public string Title { get; set; }
        public IActionResult Index()
        {
            Title = "Home";
            return View();
        
        }
        public IActionResult AboutUs()
        {
            Title = "About";
            return View();
        }
        public IActionResult ContactUs()
        {
            Title = "Contact";
            return View();
        }
    }
}
