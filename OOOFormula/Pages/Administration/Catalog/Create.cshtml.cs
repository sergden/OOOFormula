using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages.Administration.Catalog
{
    public class CreateModel : PageModel
    {
        private readonly OOOFormula.Data.ApplicationDbContext _context;

        public CreateModel(OOOFormula.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id");
        ViewData["ManufacturersId"] = new SelectList(_context.Manufacturers, "Id", "Id");
        ViewData["MaterialsId"] = new SelectList(_context.Materials, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Products Products { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Products);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
