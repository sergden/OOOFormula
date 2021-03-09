using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IMaterialsRepository
    {
        IQueryable<Materials> GetAllMater();

        Materials GetMaterial(int id);

        Materials Update(Materials updatedMater);

        Materials Add(Materials NewMater);

        Materials Delete(int id);
    }
}
