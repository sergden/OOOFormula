using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListManufacturers
{
    public class DetailsModel : PageModel
    {
        private readonly IManufacturersRepostory _db;

        public DetailsModel(IManufacturersRepostory db)
        {
            _db = db;
        }

        public Manufacturers Manufacturers { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            Manufacturers = await _db.GetManufacturer(id); //получаем запись из БД

            if (Manufacturers == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
