﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages.Administration.Catalog.ListMaterials
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Materials> Materials { get;set; }

        public async Task OnGetAsync()
        {
            Materials = await _context.Materials.ToListAsync();
        }

        public async Task OnGetSorting(SortState sortOrder = SortState.NameAsc)
        {
            Materials = await _context.Materials.ToListAsync();

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            ViewData["ImageSort"] = sortOrder == SortState.ImageAsc ? SortState.ImageDesc : SortState.ImageAsc;

            Materials = sortOrder switch
            {
                SortState.NameDesc => Materials.OrderByDescending(p => p.Name),
                SortState.PriceAsc => Materials.OrderBy(p => p.Price),
                SortState.PriceDesc => Materials.OrderByDescending(p => p.Price),
                SortState.ImageAsc => Materials.OrderBy(p => p.ImagePath),
                SortState.ImageDesc => Materials.OrderByDescending(p => p.ImagePath),
                _ => Materials.OrderBy(p => p.Name),
            };
        }
    }
}
