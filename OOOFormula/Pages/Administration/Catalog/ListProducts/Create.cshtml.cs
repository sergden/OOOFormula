using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Collections.Generic;
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

        [BindProperty]
        public IFormFileCollection Gallery_img { get; set; }

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
                Products.ImagesName = await _fileRepository.UploadFile(Photo, "Products"); //загрузка файл на сервер и запись имени файла
            }

            //загрузка галереи
            if (Gallery_img.Count != 0)
            {                
                foreach (var item in Gallery_img) //проверка типа файла
                {
                    if (!_fileRepository.CheckMIMEType(item))
                    {
                        TempData["MIMETypeErrorGal"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                        return Page();
                    }
                }
                Products.Images = new List<ProductImages>();
                foreach (var item in Gallery_img) //Добавление записей в модель
                {
                    string imageName = await _fileRepository.UploadFile(item, "Products", "Gallery");
                    ProductImages productImages = new ProductImages()
                    {
                        ProductsId = Products.Id,
                        ImageName = imageName
                    };
                    Products.Images.Add(productImages);
                }
            }

            Products = await _db.Add(Products); //создаем запись
            TempData["SuccessMessage"] = $"Запись \"{Products.Name}\" успешно создана";
            return RedirectToPage("./Index");
        }
    }
}
