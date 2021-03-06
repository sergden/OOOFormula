using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gallery> Gallery { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Gallery.Count() > 4) //выбираем последние 4 записи из таблицы
            {
                Gallery = await _context.Gallery.Skip(_context.Gallery.Count() - 4).AsNoTracking().ToListAsync();
            }
            else
            {
                Gallery = await _context.Gallery.AsNoTracking().ToListAsync();
            }
        }
    }
}
