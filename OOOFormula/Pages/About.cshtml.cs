using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages
{
    public class AboutModel : PageModel
    {
        private readonly IPagesRepository _db;

        public AboutModel(IPagesRepository db)
        {
            _db = db;
        }

        [BindProperty]
        public _Pages _page { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            _page = await _db.GetPage("About");
            if (_page == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
