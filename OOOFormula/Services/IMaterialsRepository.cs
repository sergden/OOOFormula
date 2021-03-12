using Microsoft.AspNetCore.Mvc.Rendering;
using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IMaterialsRepository
    {
        IQueryable<Materials> GetAllMater();

        Task<Materials> GetMaterial(int id);

        SelectList MaterialToList();

        Task<Materials> Update(Materials updatedMater);

        Task<Materials> Add(Materials NewMater);

        Task<Materials> Delete(int id);

        IQueryable<Materials> Sorting(IQueryable<Materials> items, SortState? sortState);

        public bool MaterialsExists(int id);
    }
}
