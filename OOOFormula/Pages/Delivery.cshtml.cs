using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages
{
    public class DeliveryModel : PageModel
    {
        private readonly IPagesRepository _db;

        public DeliveryModel(IPagesRepository db)
        {
            _db = db;
        }

        public _Pages _page { get; set; }

        public async Task<IActionResult> OnGet()
        {
            _page = await _db.GetPage("Delivery");
            if (_page == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
