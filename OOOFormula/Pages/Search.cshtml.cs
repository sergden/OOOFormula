using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SearchModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Products> Products { get; set; }
        public IEnumerable<Gallery> Gallery { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                Products = await _context.Products.Where(p =>
                                 p.Name.ToLower().Contains(searchString.ToLower()) ||
                                 p.Profile.Description.ToLower().Contains(searchString.ToLower()))
                            .Include(p=>p.Profile)
                            .AsNoTracking()
                            .ToListAsync(); //выбираем из БД записи по критерию

                Gallery = await _context.Gallery.Where(p =>
                     p.Name.ToLower().Contains(searchString.ToLower()))
               .AsNoTracking()
               .ToListAsync();

                if (!Products.Any() && !Gallery.Any()) //сообщение пользователю, если списки пустые
                {
                    TempData["Message"] = $"Ничего не найдено";
                }
                return Page();
            }
            return NotFound();
        }
    }
}
