using System;
using System.ComponentModel.DataAnnotations;

namespace OOOFormula.Models
{
    public class Gallery
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' не может быть пустым")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Фото")]
        public string ImagePath { get; set; }

        [Display(Name = "Статус")]
        public bool status { get; set; }

        [Display(Name = "Дата добавления")]
        public DateTime DateAdd { get; set; }
    }
}
