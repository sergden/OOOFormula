using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages.Administration.Catalog
{
    public class IndexModel : PageModel
    {
        private readonly OOOFormula.Data.ApplicationDbContext _context;

        public IndexModel(OOOFormula.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Products> Products { get;set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturers)
                .Include(p => p.Materials).ToListAsync();
        }
    }
}
