using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListRequests
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

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

            await changeStatus(); //меняем статус сообщения на 'Прочитано'

            return Page();
        }

        private async Task changeStatus()
        {
            if (Requests.Status == false)
            {
                Requests.Status = true;
                _context.Attach(Requests).State = EntityState.Modified; //уведомляем EF, что состояние объекта изменилось
                await _context.SaveChangesAsync(); //отправляем запрос к БД на изменение
            }
        }
    }
}
