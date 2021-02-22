using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages.Administration.ListManufacturers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Manufacturers> Manufacturers { get;set; }

        public async Task OnGetAsync(SortState sortOrder = SortState.NameAsc)
        {
            Manufacturers = await _context.Manufacturers.AsNoTracking().ToListAsync();

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

            Manufacturers = sortOrder switch
            {
                SortState.NameDesc => Manufacturers.OrderByDescending(p => p.Name),
                _ => Manufacturers.OrderBy(p => p.Name),
            };
        }

        //public async Task OnGetSorting(SortState sortOrder = SortState.NameAsc)
        //{
        //    Manufacturers = await _context.Manufacturers.AsNoTracking().ToListAsync();

        //    ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

        //    Manufacturers = sortOrder switch
        //    {
        //        SortState.NameDesc => Manufacturers.OrderByDescending(p => p.Name),
        //        _ => Manufacturers.OrderBy(p => p.Name),
        //    };
        //}
    }
}
