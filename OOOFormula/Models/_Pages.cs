using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Models
{
    public class _Pages
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя страницы")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Контент")]
        public string Body { get; set; }
    }
}
