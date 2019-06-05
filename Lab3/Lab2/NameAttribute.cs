using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab2
{
    public class NameAttribute : ValidationAttribute
    {
        public string Type { get; set; }
        public NameAttribute(string type = "Имя")
        {
            Type = type;
        }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string name = value.ToString();
                Regex regex = new Regex("^\\D[a-zA-Zа-яА-Я]{2,20}$", RegexOptions.None);
                if (regex.IsMatch(name))
                    return true;
                else
                    this.ErrorMessage = $"{Type} должно содержать только буквы русского или латинского алфавита и иметь длину не более 20 символов";
            }
            return false;
        }
    }
}
