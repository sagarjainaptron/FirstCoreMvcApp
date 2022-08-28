using Microsoft.AspNetCore.Http;
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
       [Required(ErrorMessage ="Please enter title")]
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
        [Display(Name = "Choose cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }
        [Display(Name = "Choose gallery images of your book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryModel> Gallery { get; set; }
        [Display(Name = "Upload your book in PDF format")]
        [Required]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }
    }
}
