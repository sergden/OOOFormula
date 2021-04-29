using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Catalog
{
    public class CatalogModel : PageModel
    {
        private readonly IProductsRepository _db;
        private readonly IMaterialsRepository _materials;
        private readonly IManufacturersRepostory _manuf;

        public CatalogModel(IProductsRepository db, IMaterialsRepository materials, IManufacturersRepostory manuf)
        {
            _db = db;
            _materials = materials;
            _manuf = manuf;
        }

        public PaginatedList<Products> Products { get; set; }
        public IQueryable<Products> ProductsIQ { get; set; }

        //For save state sorting
        [BindProperty]
        public SortState? SortState { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MaterialIdState { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? ManufIdState { get; set; }
        public decimal PriceFromState { get; set; }
        public decimal PriceToState { get; set; }

        public async Task<IActionResult> OnGetAsync(decimal PriceFrom,
            decimal PriceTo, SortState? sortState,
            int? MaterialId_select, int? ManufId_select,
            int? pageIndex, string searchString)
        {
            ProductsIQ = _db.GetAllProducts().Where(p => p.Profile.Status == true);
            ViewData["MaterialsId"] = _materials.MaterialToList(); //получаем материалы
            ViewData["ManufId"] = _manuf.ManufToList();

            //сохраняем состояние фильтрации
            if (sortState != null) SortState = sortState;
            if (MaterialId_select != null) MaterialIdState = MaterialId_select;
            if (ManufId_select != null) ManufIdState = ManufId_select;
            PriceFromState = PriceFrom;
            PriceToState = PriceTo;

            //обрабатываем по фильтрам
            FilterPrice(PriceFrom, PriceTo);
            if (searchString != null)
            {
                ProductsIQ = _db.SearchProduct(searchString);
            }
            if (!ProductsIQ.Any()) //проверяем, есть ли что-то
            {
                TempData["Message"] = "Ничего не найдено";
            }
            ProductsIQ = _db.Sorting(ProductsIQ, sortState); //сортировка

            //пагинация
            int pageSize = 9;
            Products = await PaginatedList<Products>.CreateAsync(
                ProductsIQ.AsNoTracking(), pageIndex ?? 1, pageSize); //вызываем метод пагинации
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
                ProductsIQ = ProductsIQ.Where(x => x.Profile.FacadeMaterialsId == MaterialIdState);
            }

            if (ManufIdState != null)
            {
                ProductsIQ = ProductsIQ.Where(x => x.Profile.FurnitureManufacturersId == ManufIdState);
            }
        }
    }
}
