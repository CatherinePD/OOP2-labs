using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class ListNotEmptyAttribute : ValidationAttribute
    {
        string Message { get; set; }
        public ListNotEmptyAttribute(string message = "Ни одного элемента из списка не выбрано")
        {
            Message = message;
        }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var list = (IEnumerable<string>)value;
                if (list.Any())
                    return true;
                else
                    this.ErrorMessage = Message;
            }
            return false;
        }
    }
}
