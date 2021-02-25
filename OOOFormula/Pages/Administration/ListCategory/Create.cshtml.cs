using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListCategory
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
        public Category Category { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Запись \"{Category.Name}\" успешно создана";

            return RedirectToPage("./Index");
        }
    }
}
