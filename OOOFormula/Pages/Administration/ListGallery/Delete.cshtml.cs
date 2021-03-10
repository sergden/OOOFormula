using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.ListGallery
{
    public class DeleteModel : PageModel
    {
        private readonly IGalleryRepository _db;
        private readonly IFilesRepository _filesRepository;

        public DeleteModel(IGalleryRepository db, IFilesRepository filesRepository)
        {
            _db = db;
            _filesRepository = filesRepository;
        }

        [BindProperty]
        public Gallery Gallery { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Gallery = await _db.GetGallery(id); //получаем запись из БД

            if (Gallery == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {            
            Gallery = await _db.Delete(id); //удаляем запись
            if (Gallery.ImagePath != null) _filesRepository.DeleteFile(Gallery.ImagePath, "Gallery"); //удаляем фото
            TempData["SuccessMessage"] = $"Запись \"{Gallery.Name}\" успешно удалена";
            return RedirectToPage("./Index");
        }
    }
}
