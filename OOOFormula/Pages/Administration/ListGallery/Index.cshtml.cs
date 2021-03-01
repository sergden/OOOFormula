using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListGallery
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gallery> Gallery { get; set; }

        public async Task OnGetAsync(SortState? sortOrder)
        {
            Gallery = await _context.Gallery.AsNoTracking().ToListAsync();
            Sorting(sortOrder);

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["DescripSort"] = sortOrder == SortState.DescriptionAsc ? SortState.DescriptionDesc : SortState.DescriptionAsc;
            ViewData["ImageSort"] = sortOrder == SortState.ImageAsc ? SortState.ImageDesc : SortState.ImageAsc;
            ViewData["StatusSort"] = sortOrder == SortState.StatusAsc ? SortState.StatusDesc : SortState.StatusAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
        }

        private void Sorting(SortState? sortOrder)
        {
            Gallery = sortOrder switch
            {
                SortState.NameAsc => Gallery.OrderBy(p => p.Name),
                SortState.NameDesc => Gallery.OrderByDescending(p => p.Name),
                SortState.DescriptionAsc => Gallery.OrderBy(p => p.Description),
                SortState.DescriptionDesc => Gallery.OrderByDescending(p => p.Description),
                SortState.ImageAsc => Gallery.OrderBy(p => p.ImagePath),
                SortState.ImageDesc => Gallery.OrderByDescending(p => p.ImagePath),
                SortState.StatusAsc => Gallery.OrderBy(p => p.status),
                SortState.StatusDesc => Gallery.OrderByDescending(p => p.status),
                SortState.DateAsc => Gallery.OrderBy(p => p.DateAdd),
                SortState.DateDesc => Gallery.OrderByDescending(p => p.DateAdd),
                _ => Gallery.OrderBy(p => p.Id),
            };
        }
    }
}
