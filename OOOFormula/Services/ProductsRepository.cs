using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Products> Add(Products NewProduct)
        {
            _context.Products.Add(NewProduct); //добавляем новый объект
            await _context.SaveChangesAsync(); //отправляем запрос к БД на сохранение
            return NewProduct;
        }

        public async Task<Products> Delete(int id)
        {
            var ProductToDelete = await _context.Products.FindAsync(id); //ищем в БД запись

            if (ProductToDelete != null)
            {
                _context.Products.Remove(ProductToDelete); //удаляем объект
                await _context.SaveChangesAsync(); //отправляем запрос к БД на удаление
            }
            return ProductToDelete;
        }

        public IQueryable<Products> GetAllProducts()
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturers)
                .Include(p => p.Materials)
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<Products> GetProduct(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturers)
                .Include(p => p.Materials)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id); //получаем из БД запись
        }

        public async Task<Products> Update(Products UpdatedProduct)
        {
            _context.Attach(UpdatedProduct).State = EntityState.Modified; //уведомляем EF, что состояние объекта изменилось
            await _context.SaveChangesAsync(); //отпраляем запрос к БД на изменение
            return UpdatedProduct;

        }

        public bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
