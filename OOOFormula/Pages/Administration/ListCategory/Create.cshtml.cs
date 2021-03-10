using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListCategory
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryRepository _db;

        public CreateModel(ICategoryRepository db)
        {
            _db = db;
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
            Category = await _db.Add(Category);
            TempData["SuccessMessage"] = $"Запись \"{Category.Name}\" успешно создана";
            return RedirectToPage("./Index");
        }
    }
}
