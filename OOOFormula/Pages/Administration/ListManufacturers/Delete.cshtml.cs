using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListManufacturers
{
    public class DeleteModel : PageModel
    {

        private readonly IManufacturersRepostory _db;

        public DeleteModel(IManufacturersRepostory db)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Manufacturers = await _db.Delete(id);
            TempData["SuccessMessage"] = $"Запись \"{Manufacturers.Name}\" успешно удалена";

            return RedirectToPage("./Index");
        }
    }
}
