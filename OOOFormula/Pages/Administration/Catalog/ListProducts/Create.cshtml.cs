using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOOFormula.Data;
using OOOFormula.Models;
using OOOFormula.Services;
using System;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListProducts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IFilesRepository _fileRepository;

        public CreateModel(ApplicationDbContext context, IFilesRepository fileRepository)
        {
            _context = context;
            _fileRepository = fileRepository;
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

            //загрузка нового фото на сервер
            if (Photo != null)
            {
                if (!_fileRepository.CheckMIMEType(Photo)) //проверка типа файла
                {
                    TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                    return Page();
                }

                Products.ImagesName = Convert.ToString(_fileRepository.UploadFile(Photo, "Products")); //загрузка файл на сервер и запись имени файла
            }

            _context.Products.Add(Products); //добавляем новый объект
            await _context.SaveChangesAsync(); //отправляем запрос к БД на сохранение

            TempData["SuccessMessage"] = $"Запись \"{Products.Name}\" успешно создана";

            return RedirectToPage("./Index");
        }
    }
}
