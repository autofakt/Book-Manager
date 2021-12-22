using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class DeleteModel : PageModel
    {
        private ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }
        public async Task OnGet(int Id)
        {
            Book = await _db.Book.FindAsync(Id);
        }

        public async Task<IActionResult> OnPost(Book Book)
        {
            _db.Book.Remove(Book);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
