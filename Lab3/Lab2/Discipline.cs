using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab2
{
    [Serializable]
    public class Discipline
    {
        [Required(ErrorMessage = "Поле названия дисциплины не заполнено")]
        public string Name { get; set; }
        public int Semester { get; set; }
        public int Course { get; set; }
        [ListNotEmpty("Поле специальности не заполнено")]
        public List<string> Specialty { get; set; }
        public int NumOfLectures { get; set; }
        public int NumOfLabs { get; set; }
        public string TypeOfControl { get; set; }
        [Required(ErrorMessage = "Информация о лекторе не добавлена")]
        public Lector lector { get; set; }

        public Discipline()
        {
            Specialty = new List<string>();
        }
        public Discipline(Lector lect) : this()
        {
            lector = lect;
        }
    }
}
