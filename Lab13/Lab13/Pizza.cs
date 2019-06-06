using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public class Pizza : IPizzaPrototype
    {
        public string Testo { get; set; }
        public string Cheese { get; set; }
        public string Ham { get; set; }
        public string Pineapple { get; set; }
        public string Souce { get; set; }
        public string Chicken { get; set; }
        public string Pepperoni { get; set; }

        public Pizza Clone()
        {
            var pizza = new Pizza();
            pizza = (Pizza)this.MemberwiseClone();
            return pizza;
        }

        private string BuildString()
        {
            var products = new List<string> { Testo, Cheese, Souce, Ham, Pineapple, Chicken, Pepperoni }
                               .Where(p => !string.IsNullOrWhiteSpace(p));

            return string.Join(", ", products.ToArray());
        }

        public override string ToString()
        {
            return $"Пицца готова! Состав: {BuildString()}";
        }
    }
}
