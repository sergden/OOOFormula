using System;
using System.ComponentModel.DataAnnotations;

namespace OOOFormula.Models
{
    public class Requests
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' не может быть пустым")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Телефон' не может быть пустым")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле 'Желаемая дата' не может быть пустым")]
        [Display(Name = "Желаемая дата замера")]
        [DataType(DataType.Date)]
        public DateTime DesiredDate { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Сообщение' не может быть пустым")]
        [Display(Name = "Сообщение")]
        public string Message { get; set; }

        public bool Status { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
    }
}
