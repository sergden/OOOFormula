using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListCategory
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryRepository _db;

        public DeleteModel(ICategoryRepository db)
        {
            _db = db;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _db.GetCategory(id); //получаем из БД запись
            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Category = await _db.Delete(id); //удаляем запись
            TempData["SuccessMessage"] = $"Запись \"{Category.Name}\" успешно удалена";
            return RedirectToPage("./Index");
        }
    }
}
