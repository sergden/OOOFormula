using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListGallery
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Gallery Gallery { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Gallery = await _context.Gallery.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id); //получаем запись из БД

            if (Gallery == null)
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

            Gallery = await _context.Gallery.FindAsync(id); //ищем запись в БД

            if (Gallery != null)
            {
                _context.Gallery.Remove(Gallery); //удаляем объект
                await _context.SaveChangesAsync(); //отправляем запрос к БД на удаление
            }

            TempData["SuccessMessage"] = $"Запись \"{Gallery.Name}\" успешно удалена";

            return RedirectToPage("./Index");
        }
    }
}
