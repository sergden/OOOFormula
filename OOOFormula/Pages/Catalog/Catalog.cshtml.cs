using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Catalog
{
    public class CatalogModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CatalogModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Products> Products { get; set; }

        public IEnumerable<Materials> Materials { get; set; }

        //For save state sorting
        [BindProperty]
        public SortState? PriceState { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MaterialIdState { get; set; }

        public async Task<IActionResult> OnGetAsync(decimal PriceFrom, decimal PriceTo, SortState? sortPrice, int? MaterialId_select)
        {
            Products = await _context.Products.Where(p => p.status == true).AsNoTracking().ToListAsync(); //извлекаем из БД все записи
            ViewData["MaterialsId"] = new SelectList(_context.Materials, "Id", "Name"); //получаем материалы

            //сохраняем состояние фильтрации
            if (sortPrice != null) PriceState = sortPrice;
            if (MaterialId_select != null) MaterialIdState = MaterialId_select;

            //обрабатываем по фильтрам
            FilterPrice(PriceFrom, PriceTo);

            if (!Products.Any()) //проверяем, есть ли что-то
            {
                TempData["Message"] = "Ничего не найдено";
                return Page();
            }

            //сортировка
            Sorting();

            return Page();
        }

        private void FilterPrice(decimal PriceFrom, decimal PriceTo)
        {
            if (PriceFrom >= 0 && PriceTo > 0)
            {
                Products = Products.Where(x => x.Price >= PriceFrom && x.Price <= PriceTo);
            }
            else if (PriceFrom > 0 && PriceTo == 0)
            {
                Products = Products.Where(x => x.Price >= PriceFrom);
            }

            if (MaterialIdState != null)
            {
                Products = Products.Where(x => x.MaterialsId == MaterialIdState);
            }
        }

        private void Sorting()
        {
            Products = PriceState switch
            {
                SortState.PriceAsc => Products.OrderBy(p => p.Price),
                SortState.PriceDesc => Products.OrderByDescending(p => p.Price),
                _ => Products.OrderBy(p => p.Id),
            };
        }
    }
}
