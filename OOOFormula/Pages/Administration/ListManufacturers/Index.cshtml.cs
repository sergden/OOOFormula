using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListManufacturers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Manufacturers> Manufacturers { get; set; }

        public async Task OnGetAsync(SortState? sortOrder)
        {
            Manufacturers = await _context.Manufacturers.AsNoTracking().ToListAsync(); //получаем записи из БД

            Sorting(sortOrder);

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
        }

        private void Sorting(SortState? sortOrder)
        {
            Manufacturers = sortOrder switch
            {
                SortState.NameAsc => Manufacturers.OrderBy(p => p.Name),
                SortState.NameDesc => Manufacturers.OrderByDescending(p => p.Name),
                _ => Manufacturers.OrderBy(p => p.Id),
            };
        }
    }
}
