using Microsoft.AspNetCore.Mvc.Rendering;
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

        SelectList ManufToList();

        Task<Manufacturers> Delete(int id);

        IQueryable<Manufacturers> Sortig(IQueryable<Manufacturers> items, SortState? sortState);

        bool ManufacturersExists(int id);
    }
}
