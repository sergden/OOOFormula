using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOOFormula.Data;
using OOOFormula.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListProducts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Products Products { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public IActionResult OnGet()
        {
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

            if (!Photo.ContentType.Contains("image"))
            {
                TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                return Page();
            }

            //загрузка нового фото на сервер
            if (Photo != null)
            {
                Products.ImagesName = ProcessUploadedFile();
            }

            _context.Products.Add(Products); //добавляем новый объект
            await _context.SaveChangesAsync(); //отправляем запрос к БД на сохранение

            TempData["SuccessMessage"] = $"Запись \"{Products.Name}\" успешно создана";

            return RedirectToPage("./Index");
        }

        //метод сохранения файла на сервере
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Products"); //webRootPath возвращает путь до каталогаа wwwroot
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
