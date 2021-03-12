using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListGallery
{
    public class CreateModel : PageModel
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IGalleryRepository _db;

        public CreateModel(IFilesRepository filesRepository, IGalleryRepository db)
        {
            _filesRepository = filesRepository;
            _db = db;
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
                if (!_filesRepository.CheckMIMEType(Photo)) //проверка типа файла
                {
                    TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                    return Page();
                }
                Gallery.ImagePath = await _filesRepository.UploadFile(Photo, "Gallery"); //загрузка файл на сервер и запись имени файла
            }
            Gallery = await _db.Add(Gallery);
            TempData["SuccessMessage"] = $"Запись \"{Gallery.Name}\" успешно создана";
            return RedirectToPage("./Index");
        }
    }
}
