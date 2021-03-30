using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OOOFormula.Models
{
    public class Products
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' не может быть пустым")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Стоимость' не может быть пустым")]
        [Display(Name = "Стоимость")]
        public int Price { get; set; }

        public Profile Profile { get; set; }
        public List<ProductImages> Images { get; set; }
    }
}
