using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListCategory
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id); //получаем из БД запись

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FindAsync(id); //ищем в БД запись

            if (Category != null)
            {
                _context.Category.Remove(Category); //удаляем объект
                await _context.SaveChangesAsync(); //отправляем запрос к БД на удаление
            }

            TempData["SuccessMessage"] = $"Запись \"{Category.Name}\" успешно удалена";

            return RedirectToPage("./Index");
        }
    }
}
