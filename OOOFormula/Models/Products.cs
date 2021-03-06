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

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Фото")]
        public string ImagesName { get; set; }

        [Display(Name = "Статус")]
        public bool Status { get; set; }


        [Required(ErrorMessage = "Поле 'Категория' не может быть пустым")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        [Required(ErrorMessage = "Поле 'Материал' не может быть пустым")]
        [Display(Name = "Материал")]
        public int MaterialsId { get; set; }
        public Materials Materials { get; set; }


        [Required(ErrorMessage = "Поле 'Производитель' не может быть пустым")]
        [Display(Name = "Производитель")]
        public int ManufacturersId { get; set; }
        public Manufacturers Manufacturers { get; set; }

    }
}
