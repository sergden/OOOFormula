using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Collections;
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

            RequestsIQ = Sorting(sortOrder, RequestsIQ); //сортировка

            int pageSize = 2; //количество элементов на странице
            Requests = await PaginatedList<Requests>.CreateAsync(
                RequestsIQ.AsNoTracking(), pageIndex ?? 1, pageSize); //вызываем метод пагинации
        }

        private static IQueryable<Requests> Sorting(SortState? sortOrder, IQueryable<Requests> RequestsIQ)
        {
            RequestsIQ = sortOrder switch
            {
                SortState.NameAsc => RequestsIQ.OrderBy(r => r.Name),
                SortState.NameDesc => RequestsIQ.OrderByDescending(r => r.Name),
                SortState.DateAsc => RequestsIQ.OrderBy(r => r.Date),
                SortState.DateDesc => RequestsIQ.OrderByDescending(r => r.Date),
                SortState.PhoneAsc => RequestsIQ.OrderBy(r => r.Phone),
                SortState.PhoneDesc => RequestsIQ.OrderByDescending(r => r.Phone),
                SortState.EmailAsc => RequestsIQ.OrderBy(r => r.Email),
                SortState.EmailDesc => RequestsIQ.OrderByDescending(r => r.Email),
                SortState.MessageAsc => RequestsIQ.OrderBy(r => r.Message),
                SortState.MessageDesc => RequestsIQ.OrderByDescending(r => r.Message),
                _ => RequestsIQ.OrderBy(r => r.Id),
            };
            return RequestsIQ;
        }
    }
}
