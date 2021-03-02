using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Collections.Generic;
using System.Linq;
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

        public async Task OnGetAsync(SortState? sortOrder)
        {
            Requests = await _context.Requests.AsNoTracking().ToListAsync(); //получаем записи из БД

            Sorting(sortOrder);

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PhoneSort"] = sortOrder == SortState.PhoneAsc ? SortState.PhoneDesc : SortState.PhoneAsc;
            ViewData["EmailSort"] = sortOrder == SortState.EmailAsc ? SortState.EmailDesc : SortState.EmailAsc;
            ViewData["MessageSort"] = sortOrder == SortState.MessageAsc ? SortState.MessageDesc : SortState.MessageAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
        }

        private void Sorting(SortState? sort)
        {
            IOrderedEnumerable<Requests> orderedEnumerables = sort switch
            {
                SortState.NameAsc => Requests.OrderBy(r => r.Name),
                SortState.NameDesc => Requests.OrderByDescending(r => r.Name),
                SortState.DateAsc => Requests.OrderBy(r => r.Date),
                SortState.DateDesc => Requests.OrderByDescending(r => r.Date),
                SortState.PhoneAsc => Requests.OrderBy(r => r.Phone),
                SortState.PhoneDesc => Requests.OrderByDescending(r => r.Phone),
                SortState.EmailAsc => Requests.OrderBy(r => r.Email),
                SortState.EmailDesc => Requests.OrderByDescending(r => r.Email),
                SortState.MessageAsc => Requests.OrderBy(r => r.Message),
                SortState.MessageDesc => Requests.OrderByDescending(r => r.Message),
                _ => Requests.OrderBy(r => r.Id),
            };
        }
    }
}
