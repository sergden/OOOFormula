using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages.Administration.ListManufacturers
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(OOOFormula.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Manufacturers Manufacturers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Manufacturers = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Id == id);

            if (Manufacturers == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
