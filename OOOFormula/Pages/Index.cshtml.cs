using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Linq;

namespace OOOFormula.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGalleryRepository _db;

        public IndexModel(IGalleryRepository db)
        {
            _db = db;
        }

        //public IEnumerable<Gallery> Gallery { get; set; }
        public IQueryable<Gallery> Gallery { get; set; }

        public void OnGet()
        {
            // IQueryable<Gallery> GalleryIQ = _db.GetAllGallery();
            Gallery = _db.GetAllGallery();
            if (Gallery.Count() > 4)
            {
                Gallery = Gallery.Skip(Gallery.Count() - 4);
            }
            // Gallery = await GalleryIQ.ToListAsync();
        }
    }
}
