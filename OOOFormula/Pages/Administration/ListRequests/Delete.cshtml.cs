using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListRequests
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Requests Requests { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Requests = await _context.Requests.FirstOrDefaultAsync(m => m.Id == id); //получаем запись из БД

            if (Requests == null)
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

            Requests = await _context.Requests.FindAsync(id); //ищем запись в БД

            if (Requests != null)
            {
                _context.Requests.Remove(Requests); //удаляем объект
                await _context.SaveChangesAsync(); //отправляем запрос к БД на удаление
            }

            return RedirectToPage("./Index");
        }
    }
}
