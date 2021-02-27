using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Models
{
    public class Requests
    {
        public int Id { get; set; }

        [Display(Name="Имя")]
        public string Name { get; set; }

        [Display(Name="Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Сообщение")]
        public string Message { get; set; }

        public bool Status { get; set; }
    }
}
