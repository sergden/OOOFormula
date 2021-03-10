using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public class ManufacturersRepostory : IManufacturersRepostory
    {
        private readonly ApplicationDbContext _context;

        public ManufacturersRepostory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Manufacturers> Add(Manufacturers NewManuf)
        {
            _context.Manufacturers.Add(NewManuf); //добавляем новый объект
            await _context.SaveChangesAsync(); //отправляем запрос к БД на добавление
            return NewManuf;
        }

        public async Task<Manufacturers> Delete(int id)
        {
            var ManufacturerToDelete = await _context.Manufacturers.FindAsync(id); //ищем запись в БД

            if (ManufacturerToDelete != null)
            {
                _context.Manufacturers.Remove(ManufacturerToDelete); //удаляем объект
                await _context.SaveChangesAsync(); //отправляем запрос к БД на удаление
            }

            return ManufacturerToDelete;
        }

        public IQueryable<Manufacturers> GetAllManuf()
        {
            return _context.Manufacturers.AsNoTracking().AsQueryable();
        }

        public async Task<Manufacturers> GetManufacturer(int id)
        {
            return await _context.Manufacturers.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Manufacturers> Update(Manufacturers updatedManuf)
        {
            _context.Attach(updatedManuf).State = EntityState.Modified; //уведомляем EF, что состояние объекта изменилось
            await _context.SaveChangesAsync(); //отправляем запрос к БД на изменение
            return updatedManuf;
        }

        public bool ManufacturersExists(int id)
        {
            return _context.Manufacturers.Any(e => e.Id == id);
        }

        public SelectList ManufToList()
        {
            return new SelectList(_context.Manufacturers, "Id", "Name");
        }
    }
}
