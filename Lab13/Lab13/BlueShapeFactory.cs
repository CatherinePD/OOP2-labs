using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab13
{
    public class BlueShapeFactory : IShapeFactory
    {
        public Circle createCircle()
        {
            Circle circle = new Circle();
            circle.Color = "голубой";
            circle.Radius = 50;
            return circle;
        }

        public Rect createRect()
        {
            Rect rect = new Rect();
            rect.Color = "голубой";
            rect.Width = 100;
            rect.Height = 100;
            return rect;
        }
    }
}
