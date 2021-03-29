using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListProducts
{
    public class DeleteModel : PageModel
    {
        private readonly IProductsRepository _db;
        private readonly IFilesRepository _fileRepository;

        public DeleteModel(IProductsRepository db, IFilesRepository fileRepository)
        {
            _db = db;
            _fileRepository = fileRepository;
        }

        [BindProperty]
        public Products Products { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Products = await _db.GetProduct(id); //получаем из БД запись
            if (Products == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Products = await _db.Delete(id); //удаление записи
            if (Products.Profile.ImagesName != null) _fileRepository.DeleteFile(Products.Profile.ImagesName, "Products"); //удаление фото
            TempData["SuccessMessage"] = $"Запись \"{Products.Name}\" успешно удалена";
            return RedirectToPage("./Index");
        }
    }
}
