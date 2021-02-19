using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;

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

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturers)
                .Include(p => p.Materials).ToListAsync();
        }        

        public async Task OnGetSorting(SortState sortOrder = SortState.NameAsc)
        {
            Products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturers)
                .Include(p => p.Materials).ToListAsync();

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            ViewData["DescripSort"] = sortOrder == SortState.DescriptionAsc ? SortState.DescriptionDesc : SortState.DescriptionAsc;
            ViewData["ImageSort"] = sortOrder == SortState.ImageAsc ? SortState.ImageDesc : SortState.ImageAsc;
            ViewData["StatusSort"] = sortOrder == SortState.StatusAsc ? SortState.StatusDesc : SortState.StatusAsc;
            ViewData["CategorySort"] = sortOrder == SortState.CategoryAsc ? SortState.CategoryDesc : SortState.CategoryAsc;
            ViewData["MaterialSort"] = sortOrder == SortState.MaterialAsc ? SortState.MaterialDesc : SortState.MaterialAsc;
            ViewData["ManufacturerSort"] = sortOrder == SortState.ManufacturerAsc ? SortState.ManufacturerDesc : SortState.ManufacturerAsc;
            ViewData["DateAddSort"] = sortOrder == SortState.DateAddAsc ? SortState.DateAddDesc : SortState.DateAddAsc;

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    Products = Products.OrderByDescending(p => p.Name);
                    break;
                case SortState.PriceAsc:
                    Products = Products.OrderBy(p => p.Price);
                    break;
                case SortState.PriceDesc:
                    Products = Products.OrderByDescending(p => p.Price);
                    break;
                case SortState.DescriptionAsc:
                    Products = Products.OrderBy(p => p.Description);
                    break;
                case SortState.DescriptionDesc:
                    Products = Products.OrderByDescending(p => p.Description);
                    break;
                case SortState.ImageAsc:
                    Products = Products.OrderBy(p => p.ImagesName);
                    break;
                case SortState.ImageDesc:
                    Products = Products.OrderByDescending(p => p.ImagesName);
                    break;
                case SortState.StatusAsc:
                    Products = Products.OrderBy(p => p.status);
                    break;
                case SortState.StatusDesc:
                    Products = Products.OrderByDescending(p => p.status);
                    break;
                case SortState.CategoryAsc:
                    Products = Products.OrderBy(p => p.Category.Name);
                    break;
                case SortState.CategoryDesc:
                    Products = Products.OrderByDescending(p => p.Category.Name);
                    break;
                case SortState.MaterialAsc:
                    Products = Products.OrderBy(p => p.Materials.Name);
                    break;
                case SortState.MaterialDesc:
                    Products = Products.OrderByDescending(p => p.Materials.Name);
                    break;
                case SortState.ManufacturerAsc:
                    Products = Products.OrderBy(p => p.Manufacturers.Name);
                    break;
                case SortState.ManufacturerDesc:
                    Products = Products.OrderByDescending(p => p.Manufacturers.Name);
                    break;
                default:
                    Products = Products.OrderBy(p => p.Name);
                    break;
            }
        }
    }
}
