﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOOFormula.Data;
using OOOFormula.Models;

namespace OOOFormula.Pages.Administration.ListManufacturers
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Manufacturers Manufacturers { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Manufacturers.Add(Manufacturers);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Запись \"{Manufacturers.Name}\" успешно создана";

            return RedirectToPage("./Index");
        }
    }
}