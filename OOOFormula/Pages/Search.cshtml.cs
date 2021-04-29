using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IProductsRepository _db;
        private readonly IGalleryRepository _db_Gal;

        public SearchModel(IProductsRepository db, IGalleryRepository db_Gal)
        {
            _db = db;
            _db_Gal = db_Gal;
        }

        public IQueryable<Products> Products { get; set; }

        public IQueryable<Gallery> Gallery { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                Products = _db.SearchProduct(searchString);

                Gallery = _db_Gal.SearchGallery(searchString);

                if (!Products.Any() && !Gallery.Any()) //сообщение пользователю, если списки пустые
                {
                    TempData["Message"] = $"Ничего не найдено";
                }
                return Page();
            }
            return NotFound();
        }
    }
}
