﻿using System;
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

        public async Task<IActionResult> OnGetAsync()
        {
            Products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturers)
                .Include(p => p.Materials).ToListAsync();

            Materials = await _context.Materials.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnGetFiltersAsync(decimal PriceFrom, decimal PriceTo, int? MaterialId, SortState? sortOrder)
        {
            if (PriceFrom >= 0 && PriceTo > 0)
            {
                Products = await _context.Products.Where(x => x.Price >= PriceFrom && x.Price <= PriceTo)
                    .Include(p => p.Category)
                    .Include(p => p.Manufacturers)
                    .Include(p => p.Materials).ToListAsync();
                Materials = _context.Materials.ToList();
            }
            else if (PriceFrom >= 0 && PriceTo == 0)
            {
                Products = await _context.Products.Where(x => x.Price >= PriceFrom)
                    .Include(p => p.Category)
                    .Include(p => p.Manufacturers)
                    .Include(p => p.Materials).ToListAsync();
                Materials = _context.Materials.ToList();
            }

            if (!Products.Any())
            {
                TempData["Message"] = "Ничего не найдено";
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
    }
}
