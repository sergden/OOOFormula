using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListMaterials
{
    public class CreateModel : PageModel
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IMaterialsRepository _db;

        public CreateModel(IFilesRepository filesRepository, IMaterialsRepository db)
        {
            _filesRepository = filesRepository;
            _db = db;
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
                if (!_filesRepository.CheckMIMEType(Photo)) //проверка типа файла
                {
                    TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                    return Page();
                }
                Materials.ImagePath = await _filesRepository.UploadFile(Photo, "Materials"); //загрузка файл на сервер и запись имени файла
            }
            Materials = await _db.Add(Materials);
            TempData["SuccessMessage"] = $"Запись \"{Materials.Name}\" успешно создана"; //сообщение пользователю
            return RedirectToPage("./Index");
        }
    }
}
