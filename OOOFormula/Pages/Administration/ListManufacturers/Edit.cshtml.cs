using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListManufacturers
{
    public class EditModel : PageModel
    {
        private readonly IManufacturersRepostory _db;

        public EditModel(IManufacturersRepostory db)
        {
            _db = db;
        }

        [BindProperty]
        public Manufacturers Manufacturers { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Manufacturers = await _db.GetManufacturer(id); //получаем запись из БД

            if (Manufacturers == null)
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
                Manufacturers = await _db.Update(Manufacturers);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.ManufacturersExists(Manufacturers.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["SuccessMessage"] = $"Запись \"{Manufacturers.Name}\" успешно обновлена";

            return RedirectToPage("./Index");
        }
    }
}
