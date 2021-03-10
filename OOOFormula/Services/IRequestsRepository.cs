using OOOFormula.Models;
using System.Linq;

namespace OOOFormula.Services
{
    public interface IRequestsRepository
    {
        IQueryable<Requests> GetAllRequests();

        Requests GetRequest(int id);

        Requests Add(Requests NewRequest);

        Requests Delete(int id);
    }
}
