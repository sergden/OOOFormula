using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages.Administration.ListGallery
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gallery> Gallery { get;set; }

        public async Task OnGetAsync()
        {
            Gallery = await _context.Gallery.ToListAsync();
        }

        public async Task OnGetSorting(SortState sortOrder = SortState.NameAsc)
        {
            Gallery = await _context.Gallery.ToListAsync();

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["DescripSort"] = sortOrder == SortState.DescriptionAsc ? SortState.DescriptionDesc : SortState.DescriptionAsc;
            ViewData["ImageSort"] = sortOrder == SortState.ImageAsc ? SortState.ImageDesc : SortState.ImageAsc;
            ViewData["DateAddSort"] = sortOrder == SortState.DateAddAsc ? SortState.DateAddDesc : SortState.DateAddAsc;

            Gallery = sortOrder switch
            {
                SortState.NameDesc => Gallery.OrderByDescending(p => p.Name),
                SortState.DescriptionAsc => Gallery.OrderBy(p => p.Description),
                SortState.DescriptionDesc => Gallery.OrderByDescending(p => p.Description),
                SortState.ImageAsc => Gallery.OrderBy(p => p.ImagePath),
                SortState.ImageDesc => Gallery.OrderByDescending(p => p.ImagePath),
                SortState.DateAddAsc => Gallery.OrderBy(p => p.DateAdd),
                SortState.DateAddDesc => Gallery.OrderBy(p => p.DateAdd),
                _ => Gallery.OrderBy(p => p.Name),
            };
        }
    }
}
