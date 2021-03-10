using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IMaterialsRepository
    {
        IQueryable<Materials> GetAllMater();

        Task<Materials> GetMaterial(int id);

        Task<Materials> Update(Materials updatedMater);

        Task<Materials> Add(Materials NewMater);

        Task<Materials> Delete(int id);

        public bool MaterialsExists(int id);
    }
}
