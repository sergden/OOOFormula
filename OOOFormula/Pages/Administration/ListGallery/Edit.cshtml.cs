using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListGallery
{
    public class EditModel : PageModel
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IGalleryRepository _db;

        public EditModel(IFilesRepository filesRepository, IGalleryRepository db)
        {
            _filesRepository = filesRepository;
            _db = db;
        }

        [BindProperty]
        public Gallery Gallery { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Gallery = await _db.GetGallery(id); //получаем запись из БД

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

            //удаление старого фото и загрузка нового на сервер
            if (Photo != null)
            {
                if (!_filesRepository.CheckMIMEType(Photo)) //проверка типа файла
                {
                    TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                    return Page();
                }

                if (Gallery.ImagePath != null)
                {
                    _filesRepository.DeleteFile(Gallery.ImagePath, "Gallery");
                }

                Gallery.ImagePath = Convert.ToString(_filesRepository.UploadFile(Photo, "Gallery")); //загрузка файл на сервер и запись имени файла
            }

            try
            {
                Gallery = await _db.Update(Gallery); //отправляем запрок к БД на изменение
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.GalleryExists(Gallery.Id))
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
    }
}