using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAllCategories();

        Category GetCategory(int id);

        Category Update(Category updatedCateg);

        Category Add(Category NewCateg);

        Category Delete(int id);
    }
}
