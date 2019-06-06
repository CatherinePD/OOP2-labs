using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public class PizzaDirector
    {
        private IPizzaBuilder _pizzaBuilder;
        public PizzaDirector(IPizzaBuilder pizzaBuilder)
        {
            _pizzaBuilder = pizzaBuilder;
        }

        public void makePepperoni()
        {
            _pizzaBuilder.reset();
            _pizzaBuilder.makeTesto("тонкое тесто");
            _pizzaBuilder.addCheese("моцарелла");
            _pizzaBuilder.addPepperoni();
            _pizzaBuilder.addSouce("пицца-соус");
        }
        public void makeHavaiiPizza()
        {
            _pizzaBuilder.reset();
            _pizzaBuilder.makeTesto("стандартное тесто");
            _pizzaBuilder.addCheese("моцарелла");
            _pizzaBuilder.addHam();
            _pizzaBuilder.addChicken();
            _pizzaBuilder.addPineapple();
            _pizzaBuilder.addSouce("cырный соус");
        }
        public void makeMargarita()
        {
            _pizzaBuilder.reset();
            _pizzaBuilder.makeTesto("стандартное тесто");
            _pizzaBuilder.addCheese("моцарелла");
            _pizzaBuilder.addSouce("пицца-соус");
        }
    }
}
