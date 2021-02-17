using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages.Administration.Catalog.ListMaterials
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
            return Page();
        }

        [BindProperty]
        public Materials Materials { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Materials.Add(Materials);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Запись \"{Materials.Name}\" успешно создана";

            return RedirectToPage("./Index");
        }
    }
}
