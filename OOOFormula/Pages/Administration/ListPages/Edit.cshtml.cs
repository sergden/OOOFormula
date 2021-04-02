using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListPages
{
    public class EditModel : PageModel
    {
        private readonly IPagesRepository _db;

        public EditModel(IPagesRepository db)
        {
            _db = db;
        }

        [BindProperty]
        public _Pages _Pages { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            _Pages = await _db.GetPage(id);

            if (_Pages == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _db.Update(_Pages);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.PagesExists(_Pages.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["SuccessMessage"] = $"Запись \"{_Pages.Name}\" успешно обновлена";
            return RedirectToPage("./Index");
        }
    }
}
