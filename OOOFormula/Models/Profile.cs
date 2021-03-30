using System.ComponentModel.DataAnnotations;

namespace OOOFormula.Models
{
    public class Profile
    {
        [Key]
        public int ProductsId { get; set; }

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


        [Required(ErrorMessage = "Поле 'Материал фасада' не может быть пустым")]
        [Display(Name = "Материал фасада")]
        public int FacadeMaterialsId { get; set; }
        public Materials FacadeMaterials { get; set; }


        [Required(ErrorMessage = "Поле 'Производитель фурнитуры' не может быть пустым")]
        [Display(Name = "Производитель фурнитуры")]
        public int FurnitureManufacturersId { get; set; }
        public Manufacturers FurnitureManufacturers { get; set; }

        public Products Products { get; set; }
    }
}
