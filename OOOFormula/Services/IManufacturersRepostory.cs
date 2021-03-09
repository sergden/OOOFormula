using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IManufacturersRepostory
    {
        IQueryable<Manufacturers> GetAllManuf();

        Manufacturers GetManufacturer(int id);

        Manufacturers Update(Manufacturers updatedManuf);

        Manufacturers Add(Manufacturers NewManuf);

        Manufacturers Delete(int id);        
    }
}
