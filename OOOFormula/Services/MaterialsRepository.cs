using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public class MaterialsRepository : IMaterialsRepository
    {
        private readonly ApplicationDbContext _context;

        public MaterialsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Materials> Add(Materials NewMater)
        {
            _context.Materials.Add(NewMater); //добавляем объект
            await _context.SaveChangesAsync(); //отправляем запрос к БД на добавление
            return NewMater;
        }

        public async Task<Materials> Delete(int id)
        {
            var MaterialToDelete = await _context.Materials.FindAsync(id); //ищем в БД нужную запись

            if (MaterialToDelete != null)
            {
                _context.Materials.Remove(MaterialToDelete); //удаляем объект
                await _context.SaveChangesAsync(); //отправляем запрос к БД на удаление
            }
            return MaterialToDelete;
        }

        public IQueryable<Materials> GetAllMater()
        {
            return _context.Materials.AsNoTracking().AsQueryable();
        }

        public async Task<Materials> GetMaterial(int id)
        {
            return await _context.Materials.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Materials> Update(Materials UpdatedMater)
        {
            _context.Attach(UpdatedMater).State = EntityState.Modified; //уведомляем EF, что состояние объекта изменилось
            await _context.SaveChangesAsync(); //запрос к БД на изменение записи
            return UpdatedMater;
        }

        public bool MaterialsExists(int id)
        {
            return _context.Materials.Any(e => e.Id == id);
        }

        public SelectList MaterialToList()
        {
            return new SelectList(_context.Materials, "Id", "Name");
        }
    }
}
