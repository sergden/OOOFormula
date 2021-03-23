using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IProductImagesRepository
    {
        //Task<IList<ProductImages>> Get(int id);

        Task<IEnumerable<ProductImages>> Add(IEnumerable<ProductImages> productImages);

        Task Delete(int id);
    }
}
