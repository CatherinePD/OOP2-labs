using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public interface IPizzaBuilder
    {
        void makeTesto(string type);
        void addCheese(string type);
        void addPineapple();
        void addPepperoni();
        void addSouce(string type);
        void addHam();
        void addChicken();
        void reset();
    }
}
