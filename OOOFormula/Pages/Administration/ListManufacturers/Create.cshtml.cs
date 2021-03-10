using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListManufacturers
{
    public class CreateModel : PageModel
    {
        private readonly IManufacturersRepostory _db;

        public CreateModel(IManufacturersRepostory db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Manufacturers Manufacturers { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Manufacturers = await _db.Add(Manufacturers);

            TempData["SuccessMessage"] = $"Запись \"{Manufacturers.Name}\" успешно создана";

            return RedirectToPage("./Index");
        }
    }
}
