using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    class PizzaBuilder : IPizzaBuilder
    {
        private Pizza _pizza = new Pizza();
        public void addCheese(string type)
        {
            _pizza.Cheese = type;
        }

        public void addChicken()
        {
            _pizza.Chicken = "курица";
        }

        public void addHam()
        {
            _pizza.Ham = "ветчина";
        }

        public void addPepperoni()
        {
            _pizza.Pepperoni = "пепперони";
        }

        public void addPineapple()
        {
            _pizza.Pineapple = "ананасы";
        }

        public void addSouce(string type)
        {
            _pizza.Souce = type;
        }

        public void makeTesto(string type)
        {
            _pizza.Testo = type;
        }
        public Pizza getPizza()
        {
            return _pizza;
        }
        public void reset()
        {
            _pizza = new Pizza();
        }
    }
}
