using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly ApplicationDbContext _context;

        public GalleryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Gallery> Add(Gallery NewGallery)
        {
            NewGallery.DateAdd = DateTime.Today;
            _context.Gallery.Add(NewGallery); //добавляем объект
            await _context.SaveChangesAsync(); //отправляем запрос к БД на добавление

            return NewGallery;
        }

        public async Task<Gallery> Delete(int id)
        {
            var GalleryToDelete = await _context.Gallery.FindAsync(id); //ищем запись в БД

            if (GalleryToDelete != null)
            {
                _context.Gallery.Remove(GalleryToDelete); //удаляем объект
                await _context.SaveChangesAsync(); //отправляем запрос к БД на удаление
            }
            return GalleryToDelete;
        }

        public IQueryable<Gallery> GetAllGallery()
        {
            return _context.Gallery.AsNoTracking().AsQueryable();
        }

        public async Task<Gallery> GetGallery(int id)
        {
            return await _context.Gallery.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id); //получаем запись из БД
        }

        public async Task<Gallery> Update(Gallery UpdatedGallery)
        {
            UpdatedGallery.DateAdd = DateTime.Today; //текущая дата
            _context.Attach(UpdatedGallery).State = EntityState.Modified; //уведомляем EF, что состояние объекта изменилось
            await _context.SaveChangesAsync(); //отправляем запрок к БД на изменение
            return UpdatedGallery;
        }

        public bool GalleryExists(int id)
        {
            return _context.Gallery.Any(e => e.Id == id);
        }
    }
}
