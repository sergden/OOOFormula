using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListMaterials
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Materials> Materials { get; set; }

        public async Task OnGetAsync(SortState? sortOrder)
        {
            Materials = await _context.Materials.AsNoTracking().ToListAsync();
            Sorting(sortOrder);

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            ViewData["ImageSort"] = sortOrder == SortState.ImageAsc ? SortState.ImageDesc : SortState.ImageAsc;

        }

        private void Sorting(SortState? sortOrder)
        {
            Materials = sortOrder switch
            {
                SortState.NameAsc => Materials.OrderBy(p => p.Name),
                SortState.NameDesc => Materials.OrderByDescending(p => p.Name),
                SortState.PriceAsc => Materials.OrderBy(p => p.Price),
                SortState.PriceDesc => Materials.OrderByDescending(p => p.Price),
                SortState.ImageAsc => Materials.OrderBy(p => p.ImagePath),
                SortState.ImageDesc => Materials.OrderByDescending(p => p.ImagePath),
                _ => Materials.OrderBy(p => p.Id),
            };
        }
    }
}
