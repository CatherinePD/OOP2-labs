using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab13
{
    public class Rect
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; }
        public override string ToString()
        {
            return $"Создан прямоугольник цвета {Color}, ширины {Width}, высоты {Height}";
        }
    }
}
