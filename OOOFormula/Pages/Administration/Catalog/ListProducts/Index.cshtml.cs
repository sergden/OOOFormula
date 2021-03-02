using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Collections.Generic;
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

        public IEnumerable<Products> Products { get; set; }

        public async Task OnGetAsync(SortState? sortOrder)
        {
            Products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturers)
                .Include(p => p.Materials)
                .AsNoTracking()
                .ToListAsync(); //получаем записи из БД

            Sorting(sortOrder); //сортировка

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            ViewData["DescripSort"] = sortOrder == SortState.DescriptionAsc ? SortState.DescriptionDesc : SortState.DescriptionAsc;
            ViewData["ImageSort"] = sortOrder == SortState.ImageAsc ? SortState.ImageDesc : SortState.ImageAsc;
            ViewData["StatusSort"] = sortOrder == SortState.StatusAsc ? SortState.StatusDesc : SortState.StatusAsc;
            ViewData["CategorySort"] = sortOrder == SortState.CategoryAsc ? SortState.CategoryDesc : SortState.CategoryAsc;
            ViewData["MaterialSort"] = sortOrder == SortState.MaterialAsc ? SortState.MaterialDesc : SortState.MaterialAsc;
            ViewData["ManufacturerSort"] = sortOrder == SortState.ManufacturerAsc ? SortState.ManufacturerDesc : SortState.ManufacturerAsc;
        }

        private void Sorting(SortState? sort)
        {
            Products = sort switch
            {
                SortState.NameAsc => Products.OrderBy(p => p.Name),
                SortState.NameDesc => Products.OrderByDescending(p => p.Name),
                SortState.PriceAsc => Products.OrderBy(p => p.Price),
                SortState.PriceDesc => Products.OrderByDescending(p => p.Price),
                SortState.DescriptionAsc => Products.OrderBy(p => p.Description),
                SortState.DescriptionDesc => Products.OrderByDescending(p => p.Description),
                SortState.ImageAsc => Products.OrderBy(p => p.ImagesName),
                SortState.ImageDesc => Products.OrderByDescending(p => p.ImagesName),
                SortState.StatusAsc => Products.OrderBy(p => p.status),
                SortState.StatusDesc => Products.OrderByDescending(p => p.status),
                SortState.CategoryAsc => Products.OrderBy(p => p.Category.Name),
                SortState.CategoryDesc => Products.OrderByDescending(p => p.Category.Name),
                SortState.MaterialAsc => Products.OrderBy(p => p.Materials.Name),
                SortState.MaterialDesc => Products.OrderByDescending(p => p.Materials.Name),
                SortState.ManufacturerAsc => Products.OrderBy(p => p.Manufacturers.Name),
                SortState.ManufacturerDesc => Products.OrderByDescending(p => p.Manufacturers.Name),
                _ => Products.OrderBy(p => p.Id),
            };
        }
    }
}
