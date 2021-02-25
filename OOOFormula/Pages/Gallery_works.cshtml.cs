using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages
{
    public class Gallery_worksModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Gallery_worksModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gallery> Gallery { get; set; }

        public async Task OnGetAsync()
        {
            Gallery = await _context.Gallery.Where(g => g.status == true).AsNoTracking().ToListAsync();
            if (!Gallery.Any())
            {
                TempData["Message"] = "Здесь пока ничего нет";
            }
        }
    }
}
