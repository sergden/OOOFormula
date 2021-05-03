using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListPages
{
    public class IndexModel : PageModel
    {
        private readonly IPagesRepository _db;

        public IndexModel(IPagesRepository db)
        {
            _db = db;
        }

        //  public IQueryable<_Pages> _Pages { get; set; }
        public PaginatedList<_Pages> _Pages { get; set; }

        public SortState? CurrentSort { get; set; }

        public async Task OnGetAsync(SortState? sortOrder, int? pageIndex)
        {
            CurrentSort = sortOrder; //сохранение состояния сортировки
            IQueryable<_Pages> _PagesIQ = _db.GetAllPages(); //получаем записи из БД

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["TitleSort"] = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;

            _PagesIQ = _db.Sorting(_PagesIQ, sortOrder); //сортировка

            int pageSize = 10; //количество элементов на странице
            _Pages = await PaginatedList<_Pages>.CreateAsync(
                _PagesIQ, pageIndex ?? 1, pageSize); //вызываем метод пагинации
        }
    }
}
