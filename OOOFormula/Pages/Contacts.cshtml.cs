using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OOOFormula.Pages
{
    public class ContactsModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            TempData["SuccessMessage"] = "Сообщение отправлено";
        }
    }
}
