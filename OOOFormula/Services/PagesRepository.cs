using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public class PagesRepository : IPagesRepository
    {
        private readonly ApplicationDbContext _context;

        public PagesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<_Pages> Add(_Pages NewPage)
        {
            _context.Pages.Add(NewPage);
            await _context.SaveChangesAsync();
            return NewPage;
        }

        public IQueryable<_Pages> GetAllPages()
        {
            return _context.Pages.AsNoTracking().AsQueryable();
        }

        public async Task<_Pages> GetPage(int id)
        {
            return await _context.Pages.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<_Pages> GetPage(string Name)
        {
            return await _context.Pages.FirstOrDefaultAsync(p => p.Name == Name);
        }

        public bool PagesExists(int id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }

        public IQueryable<_Pages> Sorting(IQueryable<_Pages> items, SortState? sortState)
        {
            items = sortState switch
            {
                SortState.NameAsc => items.OrderBy(p => p.Name),
                SortState.NameDesc => items.OrderByDescending(p => p.Name),
                SortState.TitleAsc => items.OrderBy(p => p.Title),
                SortState.TitleDesc => items.OrderByDescending(p => p.Title),
                _ => items.OrderBy(p => p.Id),
            };
            return items;
        }

        public async Task<_Pages> Update(_Pages updatedPage)
        {
            _context.Attach(updatedPage).State = EntityState.Modified; //уведомляем EF, что состояние объекта изменилось
            await _context.SaveChangesAsync(); //отправляем запрос к БД на изменение
            return updatedPage;
        }
    }
}
