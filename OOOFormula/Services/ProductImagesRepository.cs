using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public class ProductImagesRepository : IProductImagesRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductImagesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImages>> Add(IEnumerable<ProductImages> productImages)
        {
            foreach (ProductImages item in productImages)
            {
                _context.Entry(item).State = EntityState.Added;
            }
           await _context.SaveChangesAsync();

            return productImages;
        }

        public async Task Delete(int id)
        {
            _context.ProductImages.RemoveRange(_context.ProductImages.Where(x => x.ProductsId == id));
        }

        //public async Task<IList<ProductImages>> Get(int id)
        //{
        //    return await _context.ProductImages.Where(x => x.ProductsId == id).ToListAsync();
        //}
    }
}
