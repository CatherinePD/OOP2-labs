using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab13
{
    public class RedShapeFactory : IShapeFactory
    {
        public Circle createCircle()
        {
            Circle circle = new Circle();
            circle.Color = "красный";
            circle.Radius = 10;
            return circle;
        }

        public Rect createRect()
        {
            Rect rect = new Rect();
            rect.Color = "красный";
            rect.Width = 20;
            rect.Height = 20;
            return rect;
        }
    }
}
