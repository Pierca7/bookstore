using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechChallenge.Models;


namespace TechChallenge.Models
{
    public class BookInputViewModel
    {
        public Book Book { get; set; }
        public string Handler { get; set; }
        public List<SelectListItem> Genres { get; set; }

        public BookInputViewModel(string handler, Book book)
        {
            Book = book;
            Book.Link = (String.IsNullOrEmpty(Book.Link)) ? Book.Link : (Book.Link).Replace("http://", ""); //Remove http//: in edit form.
            Handler = handler;
            Genres = new List<SelectListItem> {
                new SelectListItem { Value = "Fantasy", Text = "Fantasy" },
                new SelectListItem { Value = "Horror", Text = "Horror" },
                new SelectListItem { Value = "Drama", Text = "Drama" }
            };

        }
    }
}