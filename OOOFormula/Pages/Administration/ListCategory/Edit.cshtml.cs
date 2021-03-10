using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListCategory
{
    public class EditModel : PageModel
    {
        private readonly ICategoryRepository _db;

        public EditModel(ICategoryRepository db)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _db.Update(Category); //отправляем запрос к БД на изменение
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.CategoryExists(Category.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["SuccessMessage"] = $"Запись \"{Category.Name}\" успешно обновлена";

            return RedirectToPage("./Index");
        }
    }
}
