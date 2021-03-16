using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OOOFormula.Pages
{
    public class AboutModel : PageModel
    {
        public void OnGet()
        {
            //Сообщение об отключенном JS
            if (!(Request.Cookies.ContainsKey("JavaScript") && Request.Cookies["JavaScript"] == "true"))
            {
                TempData["JsError"] = "Ваш браузер не поддерживает JavaScript. Сайт может работать неправильно";
            }
        }
    }
}
