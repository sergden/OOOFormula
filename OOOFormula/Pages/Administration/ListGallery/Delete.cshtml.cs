using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages.Administration.ListGallery
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Gallery Gallery { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Gallery = await _context.Gallery.FirstOrDefaultAsync(m => m.Id == id);

            if (Gallery == null)
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

            Gallery = await _context.Gallery.FindAsync(id);

            if (Gallery != null)
            {
                _context.Gallery.Remove(Gallery);
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = $"Запись \"{Gallery.Name}\" успешно удалена";

            return RedirectToPage("./Index");
        }
    }
}
