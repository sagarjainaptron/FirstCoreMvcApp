using Sagar.BookStore.Enums;
using Sagar.BookStore.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sagar.BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength =5)]
       /[Required(ErrorMessage ="Please enter title")]
       [MyCustomValidationAttribute(".net")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Please enter Author")]
        public string Author { get; set; }
        [StringLength(500, MinimumLength = 20)]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Please select book language")]
        public int LanguageId { get; set; }
        //[Required(ErrorMessage = "Please select book language of your choice")]
        // public List<string> MultiLanguage { get; set; }
        // public LanguageEnum LanguageEnum { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage ="Please enter total pages")]
        [Display(Name ="Total Pages")]
        public int? TotalPages { get; set; }
    }
}
