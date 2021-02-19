using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages.Catalog
{
    public class CatalogModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CatalogModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Products> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturers)
                .Include(p => p.Materials).ToListAsync();
        }
    }
}
