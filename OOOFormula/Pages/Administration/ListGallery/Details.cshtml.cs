using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListGallery
{
    public class DetailsModel : PageModel
    {
        private readonly IGalleryRepository _db;

        public DetailsModel(IGalleryRepository db)
        {
            _db = db;
        }

        public Gallery Gallery { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Gallery = await _db.GetGallery(id); //получаем запись из БД

            if (Gallery == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
