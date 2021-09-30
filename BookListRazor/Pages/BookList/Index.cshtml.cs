using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        
        // Injected by .net core using DI as the service is registerd in Startup.cs 
        // Constructor injector
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }
        // This is handler method. In MVC this is called as Action Method
        public async Task OnGet()
        {
            Books = await _db.books.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var BookFromDb = await _db.books.FindAsync(id);

            if (BookFromDb== null)
            {
                return NotFound();
            }

            _db.books.Remove(BookFromDb);
           await  _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
