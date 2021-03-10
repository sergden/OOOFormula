using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListManufacturers
{
    public class IndexModel : PageModel
    {
        private readonly IManufacturersRepostory _db;

        public IndexModel(IManufacturersRepostory db)
        {
            _db = db;
        }

        public PaginatedList<Manufacturers> Manufacturers { get; set; }

        public SortState? CurrentSort { get; set; }

        public async Task OnGetAsync(SortState? sortOrder, int? pageIndex)
        {
            CurrentSort = sortOrder; //сохранение состояния сортировки
            IQueryable<Manufacturers> ManufacturersIQ = _db.GetAllManuf(); //получаем записи из БД

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

            ManufacturersIQ = Sorting(sortOrder, ManufacturersIQ); //сортировка

            int pageSize = 2; //количество элементов на странице
            Manufacturers = await PaginatedList<Manufacturers>.CreateAsync(
                ManufacturersIQ.AsNoTracking(), pageIndex ?? 1, pageSize); //вызываем метод пагинации
        }

        private static IQueryable<Manufacturers> Sorting(SortState? sortOrder, IQueryable<Manufacturers> ManufacturersIQ)
        {
            ManufacturersIQ = sortOrder switch
            {
                SortState.NameAsc => ManufacturersIQ.OrderBy(p => p.Name),
                SortState.NameDesc => ManufacturersIQ.OrderByDescending(p => p.Name),
                _ => ManufacturersIQ.OrderBy(p => p.Id),
            };
            return ManufacturersIQ;
        }
    }
}
