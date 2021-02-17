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

namespace OOOFormula.Pages.Administration.Catalog
{
    public class EditModel : PageModel
    {
        private readonly OOOFormula.Data.ApplicationDbContext _context;

        public EditModel(OOOFormula.Data.ApplicationDbContext context)
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
           ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
           ViewData["ManufacturersId"] = new SelectList(_context.Manufacturers, "Id", "Name");
           ViewData["MaterialsId"] = new SelectList(_context.Materials, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(Products.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
