using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Data;
using OOOFormula.Models;
using System;
using System.Threading.Tasks;

namespace OOOFormula.Pages
{
    public class ContactsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ContactsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Requests Requests { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Requests.Status = false;
            Requests.Date = DateTime.Today;

            _context.Requests.Add(Requests);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Сообщение отправлено";

            return Page();
        }
    }
}
