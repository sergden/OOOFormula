using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Tests.MockData
{
    public class MockProducts
    {
        public static List<Products> GetOneProductTest(int id = 1)
        {
            List<Products> Products = new List<Products>
            {
                new Products
                {
                    Id=id,
                    Name="NAME",
                    Price=20000
                }
            };

            return Products;
        }

        public static IQueryable<Products> GetAllProductsTest()
        {
            return GetOneProductTest().AsQueryable();
        }

        public static IQueryable<Products> SearchProductsTest(string searchString)
        {
            List<Products> Prod = GetOneProductTest();
            return Prod.Where(p =>
                     p.Name.ToLower().Contains(searchString.ToLower()))
                .AsQueryable();
        }
    }
}
