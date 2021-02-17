using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOOFormula.Models;

namespace OOOFormula.Pages.Administration.Catalog.ListProducts
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["ManufacturersId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["MaterialsId"] = new SelectList(_context.Materials, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Products Products { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Products);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Запись \"{Products.Name}\" успешно создана";

            return RedirectToPage("./Index");
        }
    }
}
