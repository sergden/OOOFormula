using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListRequests
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Requests> Requests { get; set; }

        public async Task OnGetAsync()
        {
            Requests = await _context.Requests.AsNoTracking().ToListAsync();
        }
    }
}
