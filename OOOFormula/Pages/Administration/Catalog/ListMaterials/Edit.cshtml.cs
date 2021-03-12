using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListMaterials
{
    public class EditModel : PageModel
    {
        private readonly IFilesRepository _fileRepository;
        private readonly IMaterialsRepository _db;

        public EditModel(IFilesRepository fileRepository, IMaterialsRepository db)
        {
            _fileRepository = fileRepository;
            _db = db;
        }

        [BindProperty]
        public Materials Materials { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Materials = await _db.GetMaterial(id); //получаем из БД запись

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
                if (!_fileRepository.CheckMIMEType(Photo)) //проверка типа файла
                {
                    TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                    return Page();
                }

                if (Materials.ImagePath != null)
                {
                    _fileRepository.DeleteFile(Materials.ImagePath, "Materials"); //удаляем старый файл
                }
                Materials.ImagePath = await _fileRepository.UploadFile(Photo, "Materials"); //загрузка файл на сервер и запись имени файла
            }

            try
            {
                await _db.Update(Materials); //запрос к БД на изменение записи
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.MaterialsExists(Materials.Id))
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
    }
}
