using System.ComponentModel.DataAnnotations;

namespace OOOFormula.Models
{
    public class Products
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' не может быть пустым")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Стоимость' не может быть пустым")]
        public int Price { get; set; }

        public string Description { get; set; }

        public string ImagesName { get; set; }

        public bool status { get; set; }


        [Required(ErrorMessage = "Поле 'Категория' не может быть пустым")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        [Required(ErrorMessage = "Поле 'Материал' не может быть пустым")]
        public int MaterialsId { get; set; }
        public Materials Materials { get; set; }


        [Required(ErrorMessage = "Поле 'Производитель' не может быть пустым")]
        public int ManufacturersId { get; set; }
        public Manufacturers Manufacturers { get; set; }

    }
}
