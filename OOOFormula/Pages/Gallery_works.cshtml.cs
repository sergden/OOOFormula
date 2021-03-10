using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages
{
    public class Gallery_worksModel : PageModel
    {
        private readonly IGalleryRepository _db;

        public Gallery_worksModel(IGalleryRepository db)
        {
            _db = db;
        }

        public IEnumerable<Gallery> Gallery { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Gallery> GalleryIQ = _db.GetAllGallery().Where(g => g.Status == true);
            Gallery = await GalleryIQ.ToListAsync();
            if (!Gallery.Any())
            {
                TempData["Message"] = "Здесь пока ничего нет"; //сообщение пользователю
            }
        }
    }
}
