﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;
using OOOFormula.Models;
using OOOFormula.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListProducts
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
        public Products Products { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturers)
                .Include(p => p.Materials).FirstOrDefaultAsync(m => m.Id == id); //получаем из БД запись

            if (Products == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["ManufacturersId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["MaterialsId"] = new SelectList(_context.Materials, "Id", "Name");
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

                if (Products.ImagesName != null)
                {
                    _fileRepository.deleteFile(Products.ImagesName, "Products"); //удаляем старый файл
                }

                Products.ImagesName = Convert.ToString(_fileRepository.UploadFile(Photo, "Products")); //загрузка файл на сервер и запись имени файла
            }

            _context.Attach(Products).State = EntityState.Modified; //уведомляем EF, что состояние объекта изменилось

            try
            {
                await _context.SaveChangesAsync(); //отпраляем запрос к БД на изменение
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(Products.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["SuccessMessage"] = $"Запись \"{Products.Name}\" успешно обновлена";

            return RedirectToPage("./Index");
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
