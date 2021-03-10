using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGalleryRepository _db;

        public IndexModel(IGalleryRepository db)
        {
            _db = db;
        }

        public IEnumerable<Gallery> Gallery { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Gallery> GalleryIQ = _db.GetAllGallery();
            if (GalleryIQ.Count() > 4)
            {
                GalleryIQ = GalleryIQ.Skip(Gallery.Count() - 4);
            }
            Gallery = await GalleryIQ.ToListAsync();
        }
    }
}
