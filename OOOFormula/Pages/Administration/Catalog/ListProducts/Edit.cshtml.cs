using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Administration.Catalog.ListProducts
{
    public class EditModel : PageModel
    {
        private readonly IProductsRepository _db;
        private readonly ICategoryRepository _category;
        private readonly IManufacturersRepostory _manufacturers;
        private readonly IMaterialsRepository _materials;
        private readonly IFilesRepository _fileRepository;

        public EditModel(
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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Products = await _db.GetProduct(id); //получаем из БД запись

            if (Products == null)
            {
                return NotFound();
            }
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

            //удаление старого фото и загрузка нового на сервер
            if (Photo != null)
            {
                if (!_fileRepository.CheckMIMEType(Photo)) //проверка типа файла
                {
                    TempData["MIMETypeError"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                    return Page();
                }

                if (Products.ImagesName != null)
                {
                    _fileRepository.DeleteFile(Products.ImagesName, "Products"); //удаляем старый файл
                }

                Products.ImagesName = await _fileRepository.UploadFile(Photo, "Products"); //загрузка файл на сервер и запись имени файла
            }

            //загрузка галереи
            if (Gallery_img.Count != 0)
            {
                foreach (var item in Gallery_img)
                {
                    if (!_fileRepository.CheckMIMEType(item)) //проверка типа файла
                    {
                        TempData["MIMETypeErrorGal"] = "Разрешены только файлы с типом .jpg .jpeg .png .gif";
                        return Page();
                    }
                }
                foreach (var item in Products.Images)
                {
                    _fileRepository.DeleteFile(item.ImageName, "Products", "Gallery"); //удаление фото из ФС
                }
                await _db.DeleteGallery(Products.Id);//удаляем записи из таблицы
                Products.Images.Clear(); //Очищаем лист с фото
                foreach (var item in Gallery_img)
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

            try
            {
                Products = await _db.Update(Products); //отпраляем запрос к БД на изменение
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.ProductsExists(Products.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["SuccessMessage"] = $"Запись \"{Products.Name}\" успешно обновлена";
            return RedirectToPage("./Index");
        }
    }
}
