using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IProductsRepository
    {
        IQueryable<Products> GetAllProducts();

        Task<Products> GetProduct(int id);

        Task<Products> Update(Products updatedProduct);

        Task<Products> Add(Products NewProduct);

        Task<Products> Delete(int id);

        Task DeleteGallery(int id);

        IQueryable<Products> SearchProduct(string searchString);

        IQueryable<Products> Sorting(IQueryable<Products> items, SortState? sortState);

        bool ProductsExists(int id);
    }
}
