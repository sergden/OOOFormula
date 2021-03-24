using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListProducts
{
    public class IndexModel : PageModel
    {
        private readonly IProductsRepository _db;

        public IndexModel(IProductsRepository db)
        {
            _db = db;
        }

        public PaginatedList<Products> Products { get; set; }

        public SortState? CurrentSort { get; set; }

        public async Task OnGetAsync(SortState? sortOrder, int? pageIndex)
        {
            CurrentSort = sortOrder; //сохранение состояния сортировки

            IQueryable<Products> ProductsIQ = _db.GetAllProducts(); //получаем из БД записи

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            ViewData["DescripSort"] = sortOrder == SortState.DescriptionAsc ? SortState.DescriptionDesc : SortState.DescriptionAsc;
            ViewData["ImageSort"] = sortOrder == SortState.ImageAsc ? SortState.ImageDesc : SortState.ImageAsc;
            ViewData["StatusSort"] = sortOrder == SortState.StatusAsc ? SortState.StatusDesc : SortState.StatusAsc;
            ViewData["CategorySort"] = sortOrder == SortState.CategoryAsc ? SortState.CategoryDesc : SortState.CategoryAsc;
            ViewData["MaterialSort"] = sortOrder == SortState.MaterialAsc ? SortState.MaterialDesc : SortState.MaterialAsc;
            ViewData["ManufacturerSort"] = sortOrder == SortState.ManufacturerAsc ? SortState.ManufacturerDesc : SortState.ManufacturerAsc;

            ProductsIQ = _db.Sorting(ProductsIQ, sortOrder); //сортировка

            int pageSize = 10; //количество элементов на странице
            Products = await PaginatedList<Products>.CreateAsync(
                ProductsIQ.AsNoTracking(), pageIndex ?? 1, pageSize); //вызываем метод пагинации
        }
    }
}
