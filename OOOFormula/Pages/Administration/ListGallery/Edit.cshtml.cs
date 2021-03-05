﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using OOOFormula.Services;
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
        private readonly IFilesRepository _fileRepository;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IFilesRepository fileRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _fileRepository = fileRepository;
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

            //удаление старого фото и загрузка нового на сервер
            if (Photo != null)
            {
                if (!_fileRepository.checkMIMEType(Photo)) //проверка типа файла
                {
                    TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                    return Page();
                }

                if (Gallery.ImagePath != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Gallery", Gallery.ImagePath); //получаем полное имя файла

                    if (Gallery.ImagePath != "noimage.png")
                    {
                        System.IO.File.Delete(filePath);
                    }

                }

                Gallery.ImagePath = Convert.ToString(_fileRepository.UploadFile(Photo, "Gallery")); //загрузка файл на сервер и запись имени файла
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
    }
}