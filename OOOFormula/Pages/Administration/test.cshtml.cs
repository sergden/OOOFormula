using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages.Administration
{
    public class testModel : PageModel
    {
        private readonly OOOFormula.Data.ApplicationDbContext _context;

        public testModel(OOOFormula.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public OurServices OurServices { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OurServices = await _context.OurServices.FirstOrDefaultAsync(m => m.Id == id);

            if (OurServices == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
