using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        [BindProperty]
        public int ID { get; set; }
        public async Task<IActionResult> OnGet(int? Id) //might not have an id during create so we need nullable
        {
            
            Book = new Book();

            if(Id == null)
            {
                //create
                
                return Page(); 
            }

            //update
            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == Id);

            if(Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(int Id)
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                { //create
                    _db.Book.Add(Book);
                }
                else
                { //update
                    var bookFromDb = await _db.Book.FindAsync(Id);
                    bookFromDb.Name = Book.Name;
                    bookFromDb.Author = Book.Author;
                    bookFromDb.ISBN = Book.ISBN;
                }
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
