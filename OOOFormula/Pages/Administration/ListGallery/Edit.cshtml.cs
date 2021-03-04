using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListGallery
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Gallery Gallery { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Gallery = await _context.Gallery.FirstOrDefaultAsync(m => m.Id == id); //получаем запись из БД

            if (Gallery == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!Photo.ContentType.Contains("image"))
            {
                TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                return Page();
            }

            //удаление старого фото и загрузка нового на сервер
            if (Photo != null)
            {

                if (Gallery.ImagePath != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Gallery", Gallery.ImagePath); //получаем полное имя файла

                    if (Gallery.ImagePath != "noimage.png")
                    {
                        System.IO.File.Delete(filePath);
                    }

                }

                Gallery.ImagePath = ProcessUploadedFile();
            }

            _context.Attach(Gallery).State = EntityState.Modified; //уведомляем EF, что состояние объекта изменилось

            try
            {
                await _context.SaveChangesAsync(); //отправляем запрок к БД на изменение
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GalleryExists(Gallery.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["SuccessMessage"] = $"Запись \"{Gallery.Name}\" успешно обновлена";

            return RedirectToPage("./Index");
        }

        private bool GalleryExists(int id)
        {
            return _context.Gallery.Any(e => e.Id == id);
        }

        //метод сохранения файла на сервере
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Gallery"); //webRootPath возвращает путь до каталогаа wwwroot
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName; //генерация уникального имени файла
                string filePath = Path.Combine(uploadsFolder, uniqueFileName); //объединение имени файла и сгенерированного уникального имени

                //логика сохранения на сервер фото
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fs);
                }
            }

            return uniqueFileName;
        }
    }
}