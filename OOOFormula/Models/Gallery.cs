using System;
using System.ComponentModel.DataAnnotations;

namespace OOOFormula.Models
{
    public class Gallery
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' не может быть пустым")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public DateTime DateAdd { get; set; }

    }
}
