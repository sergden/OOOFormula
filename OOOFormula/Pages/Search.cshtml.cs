using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SearchModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Products> Products { get; set; }
        public IEnumerable<Gallery> Gallery { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            Products = await _context.Products.Where(p =>
                 p.Name.ToLower().Contains(searchString.ToLower()) ||
                 p.Description.ToLower().Contains(searchString.ToLower()))
            .AsNoTracking()
            .ToListAsync();

            Gallery = await _context.Gallery.Where(p =>
                 p.Name.ToLower().Contains(searchString.ToLower()))
           .AsNoTracking()
           .ToListAsync();

            if(Products==null && Gallery == null)
            {
                TempData["Message"] = $"Ничего не найдено";
            }

            return Page();
        }
    }
}
