using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Data;
using OOOFormula.Models;
using OOOFormula.Services;
using System;
using System.Threading.Tasks;

namespace OOOFormula.Pages
{
    public class ContactsModel : PageModel
    {
        private readonly IRequestsRepository _db;

        public ContactsModel(IRequestsRepository db)
        {
            _db = db;
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

            Requests = await _db.Add(Requests);

            TempData["SuccessMessage"] = "Сообщение отправлено";

            return Page();
        }
    }
}
