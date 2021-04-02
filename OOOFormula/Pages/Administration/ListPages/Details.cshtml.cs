using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListPages
{
    public class DetailsModel : PageModel
    {
        private readonly IPagesRepository _db;

        public DetailsModel(IPagesRepository db)
        {
            _db = db;
        }

        public _Pages _Pages { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            _Pages = await _db.GetPage(id);

            if (_Pages == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
