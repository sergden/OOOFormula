using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Data;
using OOOFormula.Models;
using OOOFormula.Services;
using System;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListGallery
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFilesRepository _fileRepository;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IFilesRepository fileRepository)
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
        public Gallery Gallery { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
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

                Gallery.ImagePath = Convert.ToString(_fileRepository.UploadFile(Photo, "Gallery")); //загрузка файл на сервер и запись имени файла
            }

            Gallery.DateAdd = DateTime.Today;

            _context.Gallery.Add(Gallery); //добавляем объект
            await _context.SaveChangesAsync(); //отправляем запрос к БД на добавление

            TempData["SuccessMessage"] = $"Запись \"{Gallery.Name}\" успешно создана";

            return RedirectToPage("./Index");
        }
    }
}
