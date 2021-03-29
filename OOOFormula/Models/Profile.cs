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

        public Products Products { get; set; }
    }
}
