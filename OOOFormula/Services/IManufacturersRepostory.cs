using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IManufacturersRepostory
    {
        IQueryable<Manufacturers> GetAllManuf();

        Task<Manufacturers> GetManufacturer(int id);

        Task<Manufacturers> Update(Manufacturers updatedManuf);

        Task<Manufacturers> Add(Manufacturers NewManuf);

        Task<Manufacturers> Delete(int id);

        bool ManufacturersExists(int id);
    }
}
