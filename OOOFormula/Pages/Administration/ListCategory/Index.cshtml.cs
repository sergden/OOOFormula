using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListCategory
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Category { get; set; }

        public async Task OnGetAsync(SortState? sortOrder)
        {
            Category = await _context.Category.AsNoTracking().ToListAsync();

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

            Category = sortOrder switch
            {
                SortState.NameAsc => Category.OrderBy(p => p.Name),
                SortState.NameDesc => Category.OrderByDescending(p => p.Name),
                _ => Category.OrderBy(p => p.Id),
            };
        }
    }
}
