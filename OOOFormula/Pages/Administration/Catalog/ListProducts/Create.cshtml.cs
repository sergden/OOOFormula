using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListProducts
{
    public class CreateModel : PageModel
    {
        private readonly IFilesRepository _fileRepository;
        private readonly IProductsRepository _db;
        private readonly ICategoryRepository _category;
        private readonly IManufacturersRepostory _manufacturers;
        private readonly IMaterialsRepository _materials;

        public CreateModel(
            IFilesRepository fileRepository,
            IProductsRepository db,
            ICategoryRepository category,
            IManufacturersRepostory manufacturers,
            IMaterialsRepository materials)
        {
            _fileRepository = fileRepository;
            _db = db;
            _category = category;
            _manufacturers = manufacturers;
            _materials = materials;
        }

        [BindProperty]
        public Products Products { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = _category.CategoryToList();
            ViewData["ManufacturersId"] = _manufacturers.ManufToList();
            ViewData["MaterialsId"] = _materials.MaterialToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //загрузка нового фото на сервер
            if (Photo != null)
            {
                if (!_fileRepository.CheckMIMEType(Photo)) //проверка типа файла
                {
                    TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                    return Page();
                }
                Products.ImagesName = Convert.ToString(_fileRepository.UploadFile(Photo, "Products")); //загрузка файл на сервер и запись имени файла
            }
            Products = await _db.Add(Products); //создаем запись
            TempData["SuccessMessage"] = $"Запись \"{Products.Name}\" успешно создана";
            return RedirectToPage("./Index");
        }
    }
}
