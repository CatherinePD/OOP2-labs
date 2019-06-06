using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Pizza _latestOrder;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateCircleButton_Click(object sender, RoutedEventArgs e)
        {
            IShapeFactory factory = GetFactory();

            var circle = factory.createCircle();
            tbk.Text = circle.ToString();
        }
        private void CreateRectButton_Click(object sender, RoutedEventArgs e)
        {
            IShapeFactory factory = GetFactory();

            var rect = factory.createRect();
            tbk.Text = rect.ToString();
            
        }

        private IShapeFactory GetFactory()
        {
            if (BlueRadioBut.IsChecked == true)
            {
                return KingInTheNorth.Instance.GetBlueShapeFactory();
            }
            else return KingInTheNorth.Instance.GetRedShapeFactory();
        }

        private void MakePizzaButton_Click(object sender, RoutedEventArgs e)
        {
            PizzaBuilder builder = new PizzaBuilder();
            var director = new PizzaDirector(builder);

            if (cmx.SelectedIndex == 0)
            {
                director.makeMargarita();
            }
            else if (cmx.SelectedIndex == 1)
                director.makePepperoni();
            else
                director.makeHavaiiPizza();

            var pizza = builder.getPizza();
            tbk1.Text = pizza.ToString();

            _latestOrder = pizza;
        }

        private void RepeatOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_latestOrder != null)
            {
                var clone = _latestOrder.Clone();
                tbk2.Text = $"Повтор последнего заказа... \n{clone.ToString()}";
            }
        }
    }
}
