﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
