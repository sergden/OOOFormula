using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListProducts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<Products> Products { get; set; }

        public SortState? CurrentSort { get; set; }

        public async Task OnGetAsync(SortState? sortOrder, int? pageIndex)
        {
            CurrentSort = sortOrder; //сохранение состояния сортировки

            IQueryable<Products> ProductsIQ = from s in _context.Products
                                               .Include(p => p.Category)
                                               .Include(p => p.Manufacturers)
                                               .Include(p => p.Materials)
                                              select s; //получаем из БД записи

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            ViewData["DescripSort"] = sortOrder == SortState.DescriptionAsc ? SortState.DescriptionDesc : SortState.DescriptionAsc;
            ViewData["ImageSort"] = sortOrder == SortState.ImageAsc ? SortState.ImageDesc : SortState.ImageAsc;
            ViewData["StatusSort"] = sortOrder == SortState.StatusAsc ? SortState.StatusDesc : SortState.StatusAsc;
            ViewData["CategorySort"] = sortOrder == SortState.CategoryAsc ? SortState.CategoryDesc : SortState.CategoryAsc;
            ViewData["MaterialSort"] = sortOrder == SortState.MaterialAsc ? SortState.MaterialDesc : SortState.MaterialAsc;
            ViewData["ManufacturerSort"] = sortOrder == SortState.ManufacturerAsc ? SortState.ManufacturerDesc : SortState.ManufacturerAsc;

            ProductsIQ = Sorting(sortOrder, ProductsIQ); //сортировка

            int pageSize = 2; //количество элементов на странице
            Products = await PaginatedList<Products>.CreateAsync(
                ProductsIQ.AsNoTracking(), pageIndex ?? 1, pageSize); //вызываем метод пагинации
        }

        private static IQueryable<Products> Sorting(SortState? sortOrder, IQueryable<Products> ProductsIQ)
        {
            ProductsIQ = sortOrder switch
            {
                SortState.NameAsc => ProductsIQ.OrderBy(p => p.Name),
                SortState.NameDesc => ProductsIQ.OrderByDescending(p => p.Name),
                SortState.PriceAsc => ProductsIQ.OrderBy(p => p.Price),
                SortState.PriceDesc => ProductsIQ.OrderByDescending(p => p.Price),
                SortState.DescriptionAsc => ProductsIQ.OrderBy(p => p.Description),
                SortState.DescriptionDesc => ProductsIQ.OrderByDescending(p => p.Description),
                SortState.ImageAsc => ProductsIQ.OrderBy(p => p.ImagesName),
                SortState.ImageDesc => ProductsIQ.OrderByDescending(p => p.ImagesName),
                SortState.StatusAsc => ProductsIQ.OrderBy(p => p.Status),
                SortState.StatusDesc => ProductsIQ.OrderByDescending(p => p.Status),
                SortState.CategoryAsc => ProductsIQ.OrderBy(p => p.Category.Name),
                SortState.CategoryDesc => ProductsIQ.OrderByDescending(p => p.Category.Name),
                SortState.MaterialAsc => ProductsIQ.OrderBy(p => p.Materials.Name),
                SortState.MaterialDesc => ProductsIQ.OrderByDescending(p => p.Materials.Name),
                SortState.ManufacturerAsc => ProductsIQ.OrderBy(p => p.Manufacturers.Name),
                SortState.ManufacturerDesc => ProductsIQ.OrderByDescending(p => p.Manufacturers.Name),
                _ => ProductsIQ.OrderBy(p => p.Id),
            };

            return ProductsIQ;
        }
    }
}
