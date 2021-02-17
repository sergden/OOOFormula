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
    public class DeleteModel : PageModel
    {
        private readonly OOOFormula.Data.ApplicationDbContext _context;

        public DeleteModel(OOOFormula.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Products Products { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturers)
                .Include(p => p.Materials).FirstOrDefaultAsync(m => m.Id == id);

            if (Products == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Products = await _context.Products.FindAsync(id);

            if (Products != null)
            {
                _context.Products.Remove(Products);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
