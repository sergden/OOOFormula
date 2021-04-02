using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IPagesRepository
    {
        IQueryable<_Pages> GetAllPages();

        Task<_Pages> GetPage(int id);
        Task<_Pages> GetPage(string Name);

        Task<_Pages> Update(_Pages updatedPage);

        Task<_Pages> Add(_Pages NewPage);

        IQueryable<_Pages> Sorting(IQueryable<_Pages> items, SortState? sortState);

        public bool PagesExists(int id);
    }
}
