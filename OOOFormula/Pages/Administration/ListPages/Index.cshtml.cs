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

        public IQueryable<_Pages> _Pages { get; set; }

        public SortState? CurrentSort { get; set; }

        public async Task OnGetAsync(SortState? sortOrder)
        {
            CurrentSort = sortOrder; //сохранение состояния сортировки
            _Pages = _db.GetAllPages();//получаем записи из БД

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["TitleSort"] = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;

            _Pages = _db.Sorting(_Pages, sortOrder); //сортировка
        }
    }
}
