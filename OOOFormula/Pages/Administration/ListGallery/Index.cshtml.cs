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

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    Gallery = Gallery.OrderByDescending(p => p.Name);
                    break;
                case SortState.DescriptionAsc:
                    Gallery = Gallery.OrderBy(p => p.Description);
                    break;
                case SortState.DescriptionDesc:
                    Gallery = Gallery.OrderByDescending(p => p.Description);
                    break;
                case SortState.ImageAsc:
                    Gallery = Gallery.OrderBy(p => p.ImagePath);
                    break;
                case SortState.ImageDesc:
                    Gallery = Gallery.OrderByDescending(p => p.ImagePath);
                    break;
                case SortState.DateAddAsc:
                    Gallery = Gallery.OrderBy(p => p.DateAdd);
                    break;    
                case SortState.DateAddDesc:
                    Gallery = Gallery.OrderBy(p => p.DateAdd);
                    break;
                default:
                    Gallery = Gallery.OrderBy(p => p.Name);
                    break;
            }
        }
    }
}
