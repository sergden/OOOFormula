using Microsoft.AspNetCore.Mvc.Rendering;
using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAllCategories();

        Task<Category> GetCategory(int id);

        Task<Category> Update(Category updatedCateg);

        SelectList CategoryToList();

        Task<Category> Add(Category NewCateg);

        Task<Category> Delete(int id);

        IQueryable<Category> Sorting(IQueryable<Category> items, SortState? sortState);

        bool CategoryExists(int id);
    }
}
