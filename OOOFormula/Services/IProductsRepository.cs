using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IProductsRepository
    {
        IQueryable<Products> GetAllProducts();

        Products GetProduct(int id);

        Products Update(Products updatedProduct);

        Products Add(Products NewProduct);

        Products Delete(int id);
    }
}
