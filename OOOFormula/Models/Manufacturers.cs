using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OOOFormula.Models
{
    public class Manufacturers
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' не может быть пустым")]
        [Display(Name = "Название")]
        public string Name { get; set; }


        public List<Products> Products { get; set; }
    }
}
