﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public interface IShapeFactory
    {
        Rect createRect();
        Circle createCircle();
    }
}
