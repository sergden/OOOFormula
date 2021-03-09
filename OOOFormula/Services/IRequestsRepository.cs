using OOOFormula.Models;
using System.Linq;

namespace OOOFormula.Services
{
    public interface IRequestsRepository
    {
        IQueryable<Requests> GetAllRequests();

        Requests GetRequest(int id);

        Requests Delete(int id);
    }
}
