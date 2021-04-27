using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
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

        public PaginatedList<Gallery> Gallery { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<Gallery> GalleryIQ = _db.GetAllGallery().Where(g => g.Status == true);
            if (!GalleryIQ.Any())
            {
                TempData["Message"] = "Здесь пока ничего нет"; //сообщение пользователю
            }
            int pageSize = 12; //количество элементов на странице
            Gallery = await PaginatedList<Gallery>.CreateAsync(
                GalleryIQ.AsNoTracking(), pageIndex ?? 1, pageSize); //вызываем метод пагинации
        }
    }
}
