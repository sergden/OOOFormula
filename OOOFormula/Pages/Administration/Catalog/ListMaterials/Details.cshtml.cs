using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListMaterials
{
    public class DetailsModel : PageModel
    {
        private readonly IMaterialsRepository _db;

        public DetailsModel(IMaterialsRepository db)
        {
            _db = db;
        }

        public Materials Materials { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Materials = await _db.GetMaterial(id); //получаем из БД запись

            if (Materials == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
