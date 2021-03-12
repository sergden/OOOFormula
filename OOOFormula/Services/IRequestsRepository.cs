using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IRequestsRepository
    {
        IQueryable<Requests> GetAllRequests();

        Task<Requests> GetRequest(int id);

        Task UpdateStatus(Requests UpdatedRequest);

        Task<Requests> Add(Requests NewRequest);

        Task<Requests> Delete(int id);

        IQueryable<Requests> Sorting(IQueryable<Requests> items, SortState? sortState);
    }
}
