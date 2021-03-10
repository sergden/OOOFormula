using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListCategory
{
    public class DetailsModel : PageModel
    {
        private readonly ICategoryRepository _db;

        public DetailsModel(ICategoryRepository db)
        {
            _db = db;
        }

        public Category Category { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _db.GetCategory(id); //получаем из БД запись

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
