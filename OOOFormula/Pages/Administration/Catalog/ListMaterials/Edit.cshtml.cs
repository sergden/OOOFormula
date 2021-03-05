using Microsoft.AspNetCore.Hosting;
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

namespace OOOFormula.Pages.Administration.Catalog.ListMaterials
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly FilesRepository _fileRepository;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, FilesRepository fileRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _fileRepository = fileRepository;
        }

        [BindProperty]
        public Materials Materials { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Materials = await _context.Materials.FirstOrDefaultAsync(m => m.Id == id); //получаем из БД запись

            if (Materials == null)
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

                if (Materials.ImagePath != null)
                {
                    _fileRepository.deleteFile(Materials.ImagePath, "Materials"); //удаляем старый файл

                    //string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Materials", Materials.ImagePath); //создаем полное имя файла

                    //if (Materials.ImagePath != "noimage.png") //проверяем, не используется ли заглушка
                    //{
                    //    System.IO.File.Delete(filePath);
                    //}
                }

                Materials.ImagePath = _fileRepository.UploadFile(Photo, "Materials"); //загрузка файл на сервер и запись имени файла
            }

            _context.Attach(Materials).State = EntityState.Modified; //уведомляем EF, что состояние объекта изменилось

            try
            {
                await _context.SaveChangesAsync(); //запрос к БД на изменение записи
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialsExists(Materials.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["SuccessMessage"] = $"Запись \"{Materials.Name}\" успешно обновлена";

            return RedirectToPage("./Index");
        }

        private bool MaterialsExists(int id)
        {
            return _context.Materials.Any(e => e.Id == id);
        }       
    }
}
