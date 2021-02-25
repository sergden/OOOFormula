using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListManufacturers
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
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

            _context.Manufacturers.Add(Manufacturers);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Запись \"{Manufacturers.Name}\" успешно создана";

            return RedirectToPage("./Index");
        }
    }
}
