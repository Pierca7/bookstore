using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechChallenge.Models;
using TechChallenge.BusinessLogic;

namespace TechChallenge.Pages
{
    public class IndexModel : PageModel
    {
        private IBooksLogic methods;
        private IHostingEnvironment env;

        [BindProperty]
        public List<Book> Library { get; private set; }

        public IndexModel(IBooksLogic _methods, IHostingEnvironment _env)
        {
            methods = _methods;
            env = _env;
            Library = methods.GetAllBooks();
        }

        public void OnGet(string key)
        {
            Library = methods.SortBy(Library, key);
        }

        //Create book action.
        public IActionResult OnPostCreate(Book book, IFormFile Image, bool hasImage)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            SetBook(book, Image, hasImage);
            methods.AddBook(book);
            return RedirectToPage("/Index");
        }

        //Edit book action.
        public IActionResult OnPostEdit(Book book, IFormFile Image, bool hasImage)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Console.Write(book);
            SetBook(book, Image, hasImage);
            methods.UpdateBook(book);
            return RedirectToPage("/Index");
        }

        //Delete book action.
        public IActionResult OnPostDelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            methods.RemoveBook(id);
            return RedirectToPage("/Index");
        }

        //Save image and set CoverPage. Based on https://stackoverflow.com/questions/47125439/uploading-image-asp-net-core solution.
        private void SetBook(Book book, IFormFile Image, bool hasImage)
        {
            book.Link = "http://" + book.Link;//Set link.
            string path = "", fileName = "";
            if (Image != null && Image.Length > 0 && hasImage)
            {
                fileName = Path.GetFileName(Image.FileName).Replace(" ", "_");
                path = Path.Combine(env.WebRootPath, "images/") + fileName;
                Console.Write(env.WebRootPath + "\n");
                Console.Write(path);
                using (FileStream fs = System.IO.File.Create(path))
                {
                    Image.CopyTo(fs);
                    fs.Flush();
                }
                book.CoverPage = "/images/" + fileName;
            }
            else if (Image == null || Image.Length < 0)
            {
                book.CoverPage = "/images/not-found.png";
            }
        }

    }
}
