using System;
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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Materials Materials { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Materials = await _context.Materials.FirstOrDefaultAsync(m => m.Id == id);

            if (Materials == null)
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

            Materials = await _context.Materials.FindAsync(id);

            if (Materials != null)
            {
                _context.Materials.Remove(Materials);
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = $"Запись \"{Materials.Name}\" успешно удалена";

            return RedirectToPage("./Index");
        }
    }
}
