using System.ComponentModel.DataAnnotations;

namespace OOOFormula.Models
{
    public class Manufacturers
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' не может быть пустым")]
        public string Name { get; set; }
    }
}
