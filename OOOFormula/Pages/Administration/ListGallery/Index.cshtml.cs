using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListGallery
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<Gallery> Gallery { get; set; }

        public SortState? CurrentSort { get; set; }

        public async Task OnGetAsync(SortState? sortOrder, int? pageIndex)
        {
            CurrentSort = sortOrder; //сохранение состояния сортировки

            IQueryable<Gallery> GalleryIQ = from s in _context.Gallery
                                            select s; //получаем записи из БД

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["DescripSort"] = sortOrder == SortState.DescriptionAsc ? SortState.DescriptionDesc : SortState.DescriptionAsc;
            ViewData["ImageSort"] = sortOrder == SortState.ImageAsc ? SortState.ImageDesc : SortState.ImageAsc;
            ViewData["StatusSort"] = sortOrder == SortState.StatusAsc ? SortState.StatusDesc : SortState.StatusAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;

            GalleryIQ = Sorting(sortOrder, GalleryIQ); //сортировка

            int pageSize = 2; //количество элементов на странице
            Gallery = await PaginatedList<Gallery>.CreateAsync(
                GalleryIQ.AsNoTracking(), pageIndex ?? 1, pageSize); //вызываем метод пагинации
        }

        private static IQueryable<Gallery> Sorting(SortState? sortOrder, IQueryable<Gallery> GalleryIQ)
        {
            GalleryIQ = sortOrder switch
            {
                SortState.NameAsc => GalleryIQ.OrderBy(p => p.Name),
                SortState.NameDesc => GalleryIQ.OrderByDescending(p => p.Name),
                SortState.DescriptionAsc => GalleryIQ.OrderBy(p => p.Description),
                SortState.DescriptionDesc => GalleryIQ.OrderByDescending(p => p.Description),
                SortState.ImageAsc => GalleryIQ.OrderBy(p => p.ImagePath),
                SortState.ImageDesc => GalleryIQ.OrderByDescending(p => p.ImagePath),
                SortState.StatusAsc => GalleryIQ.OrderBy(p => p.Status),
                SortState.StatusDesc => GalleryIQ.OrderByDescending(p => p.Status),
                SortState.DateAsc => GalleryIQ.OrderBy(p => p.DateAdd),
                SortState.DateDesc => GalleryIQ.OrderByDescending(p => p.DateAdd),
                _ => GalleryIQ.OrderBy(p => p.Id),
            };
            return GalleryIQ;
        }
    }
}
