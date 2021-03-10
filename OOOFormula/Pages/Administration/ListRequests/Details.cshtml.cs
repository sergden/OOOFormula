using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListRequests
{
    public class DetailsModel : PageModel
    {
        private readonly IRequestsRepository _db;

        public DetailsModel(IRequestsRepository db)
        {
            _db = db;
        }

        public Requests Requests { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Requests = await _db.GetRequest(id); //получаем запись из БД
            if (Requests == null)
            {
                return NotFound();
            }
            await _db.UpdateStatus(Requests); //меняем статус сообщения на 'Прочитано'
            return Page();
        }
    }
}
