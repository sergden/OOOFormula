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
            NewRequest.Date = DateTime.Today;

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
            return _context.Requests.AsQueryable();
        }

        public async Task<Requests> GetRequest(int id)
        {
            return await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
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
    }
}
