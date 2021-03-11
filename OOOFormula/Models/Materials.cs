using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OOOFormula.Models
{
    public class Materials
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' не может быть пустым")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Стоимость' не может быть пустым")]
        [Display(Name = "Стоимость")]
        public int Price { get; set; }

        public string ImagePath { get; set; }


        public List<Products> Products { get; set; }
    }
}
