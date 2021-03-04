using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Data;
using OOOFormula.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListGallery
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

            if (!Photo.ContentType.Contains("image"))
            {
                TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                return Page();
            }

            //загрузка нового фото на сервер
            if (Photo != null)
            {
                Gallery.ImagePath = ProcessUploadedFile();
            }

            Gallery.DateAdd = DateTime.Today;

            _context.Gallery.Add(Gallery); //добавляем объект
            await _context.SaveChangesAsync(); //отправляем запрос к БД на добавление

            TempData["SuccessMessage"] = $"Запись \"{Gallery.Name}\" успешно создана";

            return RedirectToPage("./Index");
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
