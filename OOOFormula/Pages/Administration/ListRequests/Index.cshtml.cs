using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListRequests
{
    public class IndexModel : PageModel
    {
        private readonly IRequestsRepository _db;

        public IndexModel(IRequestsRepository db)
        {
            _db = db;
        }

        public PaginatedList<Requests> Requests { get; set; }

        public SortState? CurrentSort { get; set; }

        public async Task OnGetAsync(SortState? sortOrder, int? pageIndex)
        {
            CurrentSort = sortOrder; //сохранение состояния сортировки
            IQueryable<Requests> RequestsIQ = _db.GetAllRequests(); //формируем запрос к БД

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["PhoneSort"] = sortOrder == SortState.PhoneAsc ? SortState.PhoneDesc : SortState.PhoneAsc;
            ViewData["EmailSort"] = sortOrder == SortState.EmailAsc ? SortState.EmailDesc : SortState.EmailAsc;
            ViewData["MessageSort"] = sortOrder == SortState.MessageAsc ? SortState.MessageDesc : SortState.MessageAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;

            RequestsIQ = _db.Sorting(RequestsIQ, sortOrder); //сортировка

            int pageSize = 10; //количество элементов на странице
            Requests = await PaginatedList<Requests>.CreateAsync(
                RequestsIQ.AsNoTracking(), pageIndex ?? 1, pageSize); //вызываем метод пагинации
        }
    }
}
