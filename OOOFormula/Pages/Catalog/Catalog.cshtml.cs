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

        public IEnumerable<Materials> Materials { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal PriceFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal PriceTo { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturers)
                .Include(p => p.Materials).ToListAsync();

            Materials = await _context.Materials.ToListAsync();
        }

        public void OnGetByPrice()
        {
            Products = _context.Products
       .Include(p => p.Category)
       .Include(p => p.Manufacturers)
       .Include(p => p.Materials).ToList();

            Materials = _context.Materials.ToList();

            if (Products != null)
            {
                Products = Products.Where(x => x.Price >= PriceFrom && x.Price <= PriceTo).ToList();
            }
        }
    }
}
