using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public class RequestsRepository : IRequestsRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Requests> Add(Requests NewRequest)
        {
            NewRequest.Status = false;
            NewRequest.Date = DateTime.Now;
            _context.Requests.Add(NewRequest); //добавляем объект
            await _context.SaveChangesAsync(); //отправляем запрос к БД
            return NewRequest;
        }

        public async Task<Requests> Delete(int id)
        {
            var RequestToDelete = await _context.Requests.FindAsync(id); //ищем запись в БД

            if (RequestToDelete != null)
            {
                _context.Requests.Remove(RequestToDelete); //удаляем объект
                await _context.SaveChangesAsync(); //отправляем запрос к БД на удаление
            }
            return RequestToDelete;
        }

        public IQueryable<Requests> GetAllRequests()
        {
            return _context.Requests.AsNoTracking().AsQueryable();
        }

        public async Task<Requests> GetRequest(int id)
        {
            return await _context.Requests.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateStatus(Requests UpdatedRequest)
        {
            if (UpdatedRequest.Status == false)
            {
                UpdatedRequest.Status = true;
                _context.Attach(UpdatedRequest).State = EntityState.Modified; //уведомляем EF, что состояние объекта изменилось
                await _context.SaveChangesAsync(); //отправляем запрос к БД на изменение
            }
        }

        public IQueryable<Requests> Sorting(IQueryable<Requests> items, SortState? sortState)
        {
            items = sortState switch
            {
                SortState.NameAsc => items.OrderBy(r => r.Name),
                SortState.NameDesc => items.OrderByDescending(r => r.Name),
                SortState.DateAsc => items.OrderBy(r => r.Date),
                SortState.DateDesc => items.OrderByDescending(r => r.Date),
                SortState.PhoneAsc => items.OrderBy(r => r.Phone),
                SortState.PhoneDesc => items.OrderByDescending(r => r.Phone),
                SortState.EmailAsc => items.OrderBy(r => r.Email),
                SortState.EmailDesc => items.OrderByDescending(r => r.Email),
                SortState.MessageAsc => items.OrderBy(r => r.Message),
                SortState.MessageDesc => items.OrderByDescending(r => r.Message),
                _ => items.OrderBy(r => r.Id),
            };
            return items;
        }
    }
}
