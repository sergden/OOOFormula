using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListRequests
{
    public class DeleteModel : PageModel
    {
        private readonly IRequestsRepository _db;

        public DeleteModel(IRequestsRepository db)
        {
            _db = db;
        }

        [BindProperty]
        public Requests Requests { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Requests = await _db.GetRequest(id); //получаем запись из БД
            if (Requests == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Requests DeletedRequest = await _db.Delete(id);
            if (DeletedRequest == null)
            {
                return NotFound();
            }
            TempData["SuccessMessage"] = "Запись удалена";
            return RedirectToPage("./Index");
        }
    }
}
