using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> OnGetAsync(decimal PriceFrom, decimal PriceTo, SortState? sortOrder, int? MaterialId_select, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                if (PriceFrom >= 0 && PriceTo > 0)
                {
                    Products = await _context.Products.Where(x => x.Price >= PriceFrom && x.Price <= PriceTo).ToListAsync();
                }
                else if (PriceFrom >= 0 && PriceTo == 0)
                {
                    Products = await _context.Products.Where(x => x.Price >= PriceFrom).ToListAsync();
                }

                if (MaterialId_select != null)
                {
                    Products = await _context.Products.Where(x => x.MaterialsId == MaterialId_select).ToListAsync();
                }

                ViewData["MaterialsId"] = getMaterials();

                if (!Products.Any())
                {
                    TempData["Message"] = "Ничего не найдено";
                    return Page();
                }

                if (sortOrder != null)
                {
                    Products = sortOrder switch
                    {
                        SortState.PriceAsc => Products.OrderBy(p => p.Price),
                        SortState.PriceDesc => Products.OrderByDescending(p => p.Price),
                        _ => Products.OrderBy(p => p.Price),
                    };
                }

                return Page();
            }

            Products = await _context.Products.Where(p =>
                p.Name.ToLower().Contains(searchString.ToLower()) ||
                p.Description.ToLower().Contains(searchString.ToLower()))
            .AsNoTracking()
            .ToListAsync();

            ViewData["MaterialsId"] = getMaterials();

            return Page();
        }

        public SelectList getMaterials()
        {
            return new SelectList(_context.Materials, "Id", "Name");
        }
    }
}
