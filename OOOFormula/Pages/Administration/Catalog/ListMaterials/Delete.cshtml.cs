using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListMaterials
{
    public class DeleteModel : PageModel
    {
        private readonly IMaterialsRepository _db;
        private readonly IFilesRepository _fileRepository;

        public DeleteModel(IMaterialsRepository db, IFilesRepository fileRepository)
        {
            _db = db;
            _fileRepository = fileRepository;
        }

        [BindProperty]
        public Materials Materials { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Materials = await _db.GetMaterial(id); //получаем из БД запись
            if (Materials == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Materials = await _db.Delete(id); //удаление записи
            if (Materials.ImagePath != null) _fileRepository.DeleteFile(Materials.ImagePath, "Materials"); //удаление фото
            TempData["SuccessMessage"] = $"Запись \"{Materials.Name}\" успешно удалена"; //сообщение пользователю
            return RedirectToPage("./Index");
        }
    }
}
