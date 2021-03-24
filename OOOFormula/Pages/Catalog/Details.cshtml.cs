using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Catalog
{
    public class DetailsModel : PageModel
    {
        private readonly IProductsRepository _db;

        public DetailsModel(IProductsRepository db)
        {
            _db = db;
        }

        public Products Products { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Products = await _db.GetProduct(id); //извлекаем из БД все записи каталога
            if (Products == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
