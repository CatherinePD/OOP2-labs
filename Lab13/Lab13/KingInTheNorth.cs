using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public class KingInTheNorth
    {
        private KingInTheNorth()
        { }

        private static Lazy<KingInTheNorth> _lazy = new Lazy<KingInTheNorth>(() => new KingInTheNorth());

        public static KingInTheNorth Instance => _lazy.Value;

        public IShapeFactory GetRedShapeFactory()
        {
            return new RedShapeFactory();
        }

        public IShapeFactory GetBlueShapeFactory()
        {
            return new BlueShapeFactory();
        }
    }
}
