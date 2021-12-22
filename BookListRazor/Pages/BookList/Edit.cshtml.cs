using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }
        public async Task OnGet(int Id)
        {
            Book = await _db.Book.FindAsync(Id);
        }

        public async Task<IActionResult> OnPost(int Id)
        {
            if (ModelState.IsValid)
            {  
                var bookFromDb = await _db.Book.FindAsync(Id);
                bookFromDb.Name = Book.Name;
                bookFromDb.Author = Book.Author;
                bookFromDb.ISBN = Book.ISBN;              
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
