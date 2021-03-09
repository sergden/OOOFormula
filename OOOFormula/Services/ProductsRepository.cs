using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public class ProductsRepository : IProductsRepository
    {
        public Products Add(Products NewProduct)
        {
            throw new NotImplementedException();
        }

        public Products Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Products> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Products GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Products Update(Products updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}
