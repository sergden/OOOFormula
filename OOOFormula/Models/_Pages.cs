using System.ComponentModel.DataAnnotations;

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
