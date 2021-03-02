using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListMaterials
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Materials Materials { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Materials = await _context.Materials.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id); //получаем из БД запись

            if (Materials == null)
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

            Materials = await _context.Materials.FindAsync(id); //ищем в БД нужную запись

            if (Materials != null)
            {
                _context.Materials.Remove(Materials); //удаляем объект
                await _context.SaveChangesAsync(); //отправляем запрос к БД на удаление
            }

            TempData["SuccessMessage"] = $"Запись \"{Materials.Name}\" успешно удалена"; //сообщение пользователю

            return RedirectToPage("./Index");
        }
    }
}
