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

namespace Lab14_15
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
  //позволяет объектам с несовместимыми интерфейсами работать вместе.
    #region Adapter 
    interface IPen
    {
        string Write();
    }

    class Pen : IPen
    {
        public string Write()
        {
            return "Ручкой пишет";
        }
    }//ghostly messanger

    interface IChalk
    {
        string Draw();
    }

    class Chalk : IChalk
    {
        public string Draw()
        {
            return "Мелом пишет";
        }
    }

    class Student
    {
        public string Stuff(IPen stuff)
        {
            return stuff.Write();
        }
    }



    class ChalkToPenAdapter : IPen
    {
        Chalk chalk;
        public ChalkToPenAdapter(Chalk c)
        {
            chalk = c;
        }

        public string Write()
        {
            return chalk.Draw();
        }
    }
    #endregion

    #region Decorator
    abstract class Cake
    {
        public Cake(string n)
        {
            this.Name = n;
        }

        public string Name { get; protected set; }
        public abstract int GetCost();
    }

    class AppleCake : Cake
    {
        public AppleCake() : base("Яблочный пирог")
        { }
        public override int GetCost()
        {
            return 6;
        }
    }

    class ChocolateCake : Cake
    {
        public ChocolateCake()
            : base("Шоколадный пирог")
        { }
        public override int GetCost()
        {
            return 10;
        }
    }

    abstract class CakeDecorator : Cake
    {
        protected Cake Cake;
        public CakeDecorator(string n, Cake Cake) : base(n)
        {
            this.Cake = Cake;
        }
    }

    class DeliveryCake : CakeDecorator
    {
        public DeliveryCake(Cake p)
            : base(p.Name + ", с доставкой", p)
        { }

        public override int GetCost()
        {
            return Cake.GetCost() + 5;
        }
    }

    class ThereCake : CakeDecorator
    {
        public ThereCake(Cake p)
            : base(p.Name + ", есть на месте", p)
        { }

        public override int GetCost()
        {
            return Cake.GetCost() - 1;
        }
    }
    #endregion

    #region Composite
    abstract class Component
    {
        protected string name;

        public Component(string name)
        {
            this.name = name;
        }

        public virtual void Add(Component component) { }

        public virtual void Remove(Component component) { }

        public virtual string Search(string itemName)
        {
            return "";
        }
        public virtual string GetName()
        {
            return "";
        }

        public virtual string Print()
        {
            return name;
        }
    }

    class Box : Component
    {
        private List<Component> components = new List<Component>();

        public Box(string name)
            : base(name)
        {
        }

        public override void Add(Component component)
        {
            components.Add(component);
        }

        public override void Remove(Component component)
        {
            components.Remove(component);
        }

        public override string GetName()
        {
            return name;
        }

        public override string Search(string itemName)
        {
            string result = "";

            try
            {
                for (int i = 0; i < components.Count; i++)
                {
                    foreach (Item item in (components[i] as Box).components)
                    {
                        if (item.GetName() == itemName)
                        {
                            result = "item found";
                        }
                    }
                    if (components[i].GetName() == itemName)
                    {
                        result = "item found";
                    }
                }
            }
            catch
            {

            }
            if (result == "")
            {
                result = "item not found";
            }

            return result;
        }

        public override string Print()
        {
            string message = "";
            message += name.ToUpper() + "\n";
            for (int i = 0; i < components.Count; i++)
            {
                message += "- " + components[i].Print() + "\n";
            }
            return message;
        }
    }

    class Item : Component
    {
        public Item(string name)
                : base(name)
        { }

        public override string Print()
        {
            return name;
        }

        public override string GetName()
        {
            return name;
        }

    }
    #endregion

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KeyDown += MainWindow_KeyDown; ;
        }

        private void Adapter_Button_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student();

            Pen pen = new Pen();
            AdapterTxtbl.Text += student.Stuff(pen) + '\n';
            AdapterTxtbl.Text += "Оп... В ручке стержень закончился \n";

            Chalk chalk = new Chalk();
            IPen chalkStuff = new ChalkToPenAdapter(chalk);
            AdapterTxtbl.Text += student.Stuff(chalkStuff) + '\n';
        }

        private void Decorator_Button_Click(object sender, RoutedEventArgs e)
        {
            Cake Cake1 = new AppleCake();
            Cake1 = new DeliveryCake(Cake1);
            DecoratorTxtbl.Text += String.Format("Название: {0}\n", Cake1.Name);
            DecoratorTxtbl.Text += String.Format("Цена: {0}\n", Cake1.GetCost());

            Cake Cake2 = new AppleCake();
            Cake2 = new ThereCake(Cake2);
            DecoratorTxtbl.Text += String.Format("Название: {0}\n", Cake2.Name);
            DecoratorTxtbl.Text += String.Format("Цена: {0}\n", Cake2.GetCost());

            Cake Cake3 = new ChocolateCake();
            Cake3 = new DeliveryCake(Cake3);
            DecoratorTxtbl.Text += String.Format("Название: {0}\n", Cake3.Name);
            DecoratorTxtbl.Text += String.Format("Цена: {0}\n", Cake3.GetCost());

            Cake Cake4 = new ChocolateCake();
            Cake4 = new ThereCake(Cake4);
            DecoratorTxtbl.Text += String.Format("Название: {0}\n", Cake4.Name);
            DecoratorTxtbl.Text += String.Format("Цена: {0}\n", Cake4.GetCost());
        }

        Component Box = new Box("big box:");

        private void Composite_Button_Click(object sender, RoutedEventArgs e)
        {

            Component smallBox = new Box("small box");
            Component Item1 = new Item("book");
            Component Item2 = new Item("mobile");

            smallBox.Add(Item1);
            smallBox.Add(Item2);

            Box.Add(smallBox);
            CompositeTxtbl.Items.Add(Box.Print());


            smallBox.Remove(Item1);

            Component newBox = new Box("Gift box");

            Component Item3 = new Item("Candy");
            Component Item4 = new Item("Letter");
            newBox.Add(Item3);
            newBox.Add(Item4);
            Box.Add(newBox);

            CompositeTxtbl.Items.Add(Box.Print());
        }

        private void Find_Button_Click(object sender, RoutedEventArgs e)
        {
            CompositeTxtbl.Items.Add(Box.Search("Letter"));
        }

        private void Find2_Button_Click(object sender, RoutedEventArgs e)
        {
            CompositeTxtbl.Items.Add(Box.Search("Toy"));
        }


        //-----------------------------------------------------------------
        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                MainWindow.JumpCommand jump = new JumpCommand();
                jump.Execute();
            }

            if (e.Key == Key.W)
            {
                ClimbCommand climb = new ClimbCommand();
                climb.Execute();
            }
            if (e.Key == Key.F)
            {
                FireCommand fire = new FireCommand();
                fire.Execute();
            }
        }




        #region Command
        interface ICommand
        {
            void Execute();
        };

        public class JumpCommand : ICommand
        {
            public void Execute()
            {
                Jump();
            }

            private void Jump()
            {
                MessageBox.Show("Прыжок!");
            }
        };

        public class FireCommand : ICommand
        {
            public void Execute()
            {
                Fire();
            }

            private void Fire()
            {
                MessageBox.Show("Огонь!");
            }
        };

        public class ClimbCommand : ICommand
        {
            public void Execute()
            {
                Climb();
            }

            private void Climb()
            {
                MessageBox.Show("Подниматься!");
            }
        };
        #endregion

        #region State
        interface IWaterState
        {
            void Heat(Water water);
            void Frost(Water water);
        }

        class Water
        {
            public IWaterState State { private get; set; }

            public Water(IWaterState ws)
            {
                State = ws;
            }

            public void Heat()
            {
                State.Heat(this);
            }
            public void Frost()
            {
                State.Frost(this);
            }
        }

        class SolidWaterState : IWaterState
        {
            public void Heat(Water water)
            {
                MessageBox.Show("Превращаем лед в жидкость");
                water.State = new LiquidWaterState();
            }

            public void Frost(Water water)
            {
                MessageBox.Show("Продолжаем заморозку льда");
            }
        }
        class LiquidWaterState : IWaterState
        {
            public void Heat(Water water)
            {
                MessageBox.Show("Превращаем жидкость в пар");
                water.State = new GasWaterState();
            }

            public void Frost(Water water)
            {
                MessageBox.Show("Превращаем жидкость в лед");
                water.State = new SolidWaterState();
            }
        }
        class GasWaterState : IWaterState
        {
            public void Heat(Water water)
            {
                MessageBox.Show("Повышаем температуру водяного пара");
            }

            public void Frost(Water water)
            {
                MessageBox.Show("Превращаем водяной пар в жидкость");
                water.State = new LiquidWaterState();
            }
        }
        #endregion

        #region Save
        public class PersonState
        {
            public string name { get; }
            public int age { get; }

            public PersonState(string Name, int Age)
            {
                name = Name;
                age = Age;
            }
        }

        public class Person
        {
            public string _name = "Jokar";
            public int _age = 5;

            public void SetName(string name)
            {
                _name = name;
            }

            public void SetAge(int age)
            {
                _age = age;
            }

            public MainWindow.PersonState GetState()
            {
                var person = new PersonState(_name, _age);
                return person;
            }

            public void SetState(PersonState state)
            {
                _name = state.name;
                _age = state.age;
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var p = new Person();
            p.SetName("Kate");
            p.SetAge(19);
            MessageBox.Show(p._name + "" + p._age);
            var memento = p.GetState();
            p.SetAge(20);
            MessageBox.Show(p._name + "" + p._age);
            p.SetState(memento);
            MessageBox.Show(p._name + "" + p._age);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Water water = new Water(new LiquidWaterState());
            water.Heat();
            water.Frost();
            water.Frost();
            water.Frost();
        }
    }
}
