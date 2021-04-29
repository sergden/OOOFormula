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
        private readonly IPagesRepository _db_pages;

        public ContactsModel(IRequestsRepository db,
            IGoogleRecaptchaRepository recaptcha,
            IPagesRepository db_pages)
        {
            _db = db;
            _recaptcha = recaptcha;
            _db_pages = db_pages;
        }

        [BindProperty]
        public Requests Requests { get; set; }

        public _Pages _page { get; set; }

        public async Task<IActionResult> OnGet()
        {
            _page = await _db_pages.GetPage("Contacts");
            if (_page == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _page = await _db_pages.GetPage("Contacts");
                return Page();
            }

            var CaptchaResponse = await _recaptcha.Validate(Request.Form);
            if (!CaptchaResponse.Success)
            {
                ModelState.AddModelError("reCaptchaError", "Подтвердите, что вы человек");
                _page = await _db_pages.GetPage("Contacts");
                return Page();
            }

            Requests = await _db.Add(Requests);

            TempData["SuccessMessage"] = "Сообщение отправлено";

            _page = await _db_pages.GetPage("Contacts");
            return Page();
        }
    }
}
