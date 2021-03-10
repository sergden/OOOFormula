using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListMaterials
{
    public class IndexModel : PageModel
    {
        private readonly IMaterialsRepository _db;

        public IndexModel(IMaterialsRepository db)
        {
            _db = db;
        }

        public PaginatedList<Materials> Materials { get; set; }

        public SortState? CurrentSort { get; set; }

        public async Task OnGetAsync(SortState? sortOrder, int? pageIndex)
        {
            CurrentSort = sortOrder; //сохранение состояния сортировки

            IQueryable<Materials> MaterialsIQ = _db.GetAllMater(); //получаем из БД записи

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PriceSort"] = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            ViewData["ImageSort"] = sortOrder == SortState.ImageAsc ? SortState.ImageDesc : SortState.ImageAsc;

            MaterialsIQ = Sorting(sortOrder, MaterialsIQ); //сортировка

            int pageSize = 2; //количество элементов на странице
            Materials = await PaginatedList<Materials>.CreateAsync(
                MaterialsIQ.AsNoTracking(), pageIndex ?? 1, pageSize); //вызываем метод пагинации
        }

        private static IQueryable<Materials> Sorting(SortState? sortOrder, IQueryable<Materials> MaterialsIQ)
        {
            MaterialsIQ = sortOrder switch
            {
                SortState.NameAsc => MaterialsIQ.OrderBy(p => p.Name),
                SortState.NameDesc => MaterialsIQ.OrderByDescending(p => p.Name),
                SortState.PriceAsc => MaterialsIQ.OrderBy(p => p.Price),
                SortState.PriceDesc => MaterialsIQ.OrderByDescending(p => p.Price),
                SortState.ImageAsc => MaterialsIQ.OrderBy(p => p.ImagePath),
                SortState.ImageDesc => MaterialsIQ.OrderByDescending(p => p.ImagePath),
                _ => MaterialsIQ.OrderBy(p => p.Id),
            };
            return MaterialsIQ;
        }
    }
}
