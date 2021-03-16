using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages.Catalog
{
    public class DetailsModel : PageModel
    {
        private readonly IProductsRepository _db;

        public DetailsModel(IProductsRepository db)
        {
            _db = db;
        }

        public Products Products { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            //Сообщение об отключенном JS
            if (!(Request.Cookies.ContainsKey("JavaScript") && Request.Cookies["JavaScript"] == "true"))
            {
                TempData["JsError"] = "Ваш браузер не поддерживает JavaScript. Сайт может работать неправильно";
            }

            Products = await _db.GetProduct(id); //извлекаем из БД все записи каталога, а также производителя и материал
            if (Products == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
