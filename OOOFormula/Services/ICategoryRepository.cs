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

        Task<Category> Add(Category NewCateg);

        Task<Category> Delete(int id);

        bool CategoryExists(int id);
    }
}
