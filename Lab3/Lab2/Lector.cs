using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab2
{
    [Serializable]
    public class Lector
    {
        [Name]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле имени не заполнено")]
        public string Name { get; set; }
        [Name("Фамилия")]
        [Required(ErrorMessage = "Поле фамилии не заполнено")]
        public string Surname { get; set; }
        [Name("Отчество")]
        [Required(ErrorMessage = "Поле отчества не заполнено")]
        public string Patronomyc { get; set; }
        [Required(ErrorMessage = "Поле кафедры не заполнено")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Поле аудитории не заполнено")]
        public string Auditory { get; set; }
        [Required(ErrorMessage = "Поле контактного номера не заполнено")]
        [Phone(ErrorMessage = "Контактный номер введен неверно")]
        public string Number { get; set; }
    }
}
