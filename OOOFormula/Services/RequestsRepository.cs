using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using OOOFormula.Data;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public class RequestsRepository : IRequestsRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Requests Add(Requests NewRequest)
        {
            throw new NotImplementedException();
        }

        public Requests Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Requests> GetAllRequests()
        {
            return _context.Requests.AsQueryable();
        }

        public Requests GetRequest(int id)
        {
            throw new NotImplementedException();
        }
    }
}
