using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OOOFormula.Models;
using OOOFormula.Services;
using System.Threading.Tasks;

namespace OOOFormula.Pages
{
    public class ContactsModel : PageModel
    {
        private readonly IRequestsRepository _db;
        private readonly IGoogleRecaptchaRepository _recaptcha;

        public ContactsModel(IRequestsRepository db, IGoogleRecaptchaRepository recaptcha)
        {
            _db = db;
            _recaptcha = recaptcha;
        }

        [BindProperty]
        public Requests Requests { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var CaptchaResponse = await _recaptcha.Validate(Request.Form);
            if (!CaptchaResponse.Success)
            {
                ModelState.AddModelError("reCaptchaError", "Подтвердите, что вы человек");
                return Page();
            }

            Requests = await _db.Add(Requests);

            TempData["SuccessMessage"] = "Сообщение отправлено";

            return Page();
        }
    }
}
