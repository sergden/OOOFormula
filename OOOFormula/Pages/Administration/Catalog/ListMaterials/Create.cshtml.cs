using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Data;
using OOOFormula.Models;
using OOOFormula.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListMaterials
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly FilesRepository _fileRepository;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, FilesRepository fileRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _fileRepository = fileRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Materials Materials { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

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
                if (!_fileRepository.checkMIMEType(Photo)) //проверка типа файла
                {
                    TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                    return Page();
                }

                Materials.ImagePath = _fileRepository.UploadFile(Photo, "Materials"); //загрузка файл на сервер и запись имени файла
            }

            _context.Materials.Add(Materials); //добавляем объект
            await _context.SaveChangesAsync(); //отправляем запрос к БД на добавление

            TempData["SuccessMessage"] = $"Запись \"{Materials.Name}\" успешно создана"; //сообщение пользователю

            return RedirectToPage("./Index");
        }
    }
}
