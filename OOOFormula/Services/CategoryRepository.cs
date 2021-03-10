using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> Add(Category NewCateg)
        {
            _context.Category.Add(NewCateg); //добавляем объект
            await _context.SaveChangesAsync(); //отправляем запрос к БД на сохранение
            return NewCateg;
        }

        public async Task<Category> Delete(int id)
        {
            var CategoryToDelete = await _context.Category.FindAsync(id); //ищем в БД запись

            if (CategoryToDelete != null)
            {
                _context.Category.Remove(CategoryToDelete); //удаляем объект
                await _context.SaveChangesAsync(); //отправляем запрос к БД на удаление
            }
            return CategoryToDelete;
        }

        public IQueryable<Category> GetAllCategories()
        {
            return _context.Category.AsNoTracking().AsQueryable();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Category.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id); //получаем из БД запись
        }

        public async Task<Category> Update(Category UpdatedCateg)
        {
            _context.Attach(UpdatedCateg).State = EntityState.Modified; //уведомляем EF, что состояние объекта изменилось
            await _context.SaveChangesAsync(); //отправляем запрос к БД на изменение
            return UpdatedCateg;

        }

        public bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
