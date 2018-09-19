using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace TechChallenge.Models
{
    public class Book
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "A title is required")]
        [StringLength(128)]
        public string Title { get; set; }

        [Required(ErrorMessage = "An author name is required")]
        [StringLength(64)]
        public string Author { get; set; }

        [Required(ErrorMessage = "A brief summary of the book is required")]
        [StringLength(256)]
        public string Summary { get; set; }

        [Required(ErrorMessage = "The genre of the book is required")]
        [StringLength(24)]
        public string Genre { get; set; }

        public string CoverPage { get; set; }

        [Required(ErrorMessage = "A publication date is required")]
        [Range(1, 9999)]
        public int Published { get; set; }

        [Required(ErrorMessage = "A link to the bookseller's website is required")]
        public string Link { get; set; }
    }


}