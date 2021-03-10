using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Catalog
{
    public class CatalogModel : PageModel
    {
        private readonly IProductsRepository _db;
        private readonly IMaterialsRepository _materials;

        public CatalogModel(IProductsRepository db, IMaterialsRepository materials)
        {
            _db = db;
            _materials = materials;
        }

        public IEnumerable<Products> Products { get; set; }
        public IQueryable<Products> ProductsIQ { get; set; }

        public IEnumerable<Materials> Materials { get; set; }

        //For save state sorting
        [BindProperty]
        public SortState? PriceState { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MaterialIdState { get; set; }

        public async Task<IActionResult> OnGetAsync(decimal PriceFrom, decimal PriceTo, SortState? sortPrice, int? MaterialId_select)
        {
            ProductsIQ = _db.GetAllProducts().Where(p => p.Status == true);
            ViewData["MaterialsId"] = _materials.MaterialToList(); //получаем материалы

            //сохраняем состояние фильтрации
            if (sortPrice != null) PriceState = sortPrice;
            if (MaterialId_select != null) MaterialIdState = MaterialId_select;

            //обрабатываем по фильтрам
            FilterPrice(PriceFrom, PriceTo);
            if (!ProductsIQ.Any()) //проверяем, есть ли что-то
            {
                TempData["Message"] = "Ничего не найдено";
                return Page();
            }
            //сортировка
            Sorting();

            Products = await ProductsIQ.ToListAsync();
            return Page();
        }

        private void FilterPrice(decimal PriceFrom, decimal PriceTo)
        {
            if (PriceFrom >= 0 && PriceTo > 0)
            {
                ProductsIQ = ProductsIQ.Where(x => x.Price >= PriceFrom && x.Price <= PriceTo);
            }
            else if (PriceFrom > 0 && PriceTo == 0)
            {
                ProductsIQ = ProductsIQ.Where(x => x.Price >= PriceFrom);
            }

            if (MaterialIdState != null)
            {
                ProductsIQ = ProductsIQ.Where(x => x.MaterialsId == MaterialIdState);
            }
        }

        private void Sorting()
        {
            ProductsIQ = PriceState switch
            {
                SortState.PriceAsc => ProductsIQ.OrderBy(p => p.Price),
                SortState.PriceDesc => ProductsIQ.OrderByDescending(p => p.Price),
                _ => ProductsIQ.OrderBy(p => p.Id),
            };
        }
    }
}
