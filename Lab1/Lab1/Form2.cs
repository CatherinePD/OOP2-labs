using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form2 : Form
    { 
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            e.Graphics.DrawLine(pen, 350, 552, 350, 0);
        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Person(string n, int a)
            {
                Name = n;
                Age = a;
            }
            public override string ToString()
            {
                return $"Name: {Name}, Age: {Age}";
            }
        }
        string[] NamesArr = {"Katrin", "Alexandra", "Olga", "Darya", "Lizaveta", "Nastya",
        "Eygen", "Alexey", "Dmitriy", "Nikita", "James", "John", "Mary", "Anna", "Daniil", "Kirill",
        "Vlada", "Kostya", "Chloe", "Max", "Egor", "Galina", "Artem", "Polina", "Julia"};

        private int Number;

        private string Letter;

        private List<Person> ColOfPeople = new List<Person>();
        
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            ColOfPeople.Clear();
            listViewCol.Clear();
            if (string.IsNullOrEmpty(textBoxColSize.Text))
                return;
            try
            {
                Number = int.Parse(textBoxColSize.Text);
            }
            catch (Exception ex)
            {
                textBoxColSize.Clear();
                MessageBox.Show("Значение размера коллекции должно быть числовым");
            }

            if (Number > 0 && Number < 20)
            {
                for (int x = 0; x < Number; x++) //кол-во итер=кол-ву эл-тов в кол
                {
                    Thread.Sleep(30);
                    //занесение в кол объекта с рандомным именем из массива и рандомным возрастом
                    ColOfPeople.Add(new Person(NamesArr[new Random().Next(0, NamesArr.Length)], new Random().Next(2, 90)));
                    listViewCol.Items.Add(ColOfPeople[x].ToString());
                }
            }

        }

        private void buttonOrderByAsc_Click(object sender, EventArgs e)
        {
            listViewOrdCol.Clear();
            var OrdCol = ColOfPeople.OrderBy(a => a.Age);
            foreach(Person item in OrdCol)
            {
                listViewOrdCol.Items.Add(item.ToString());
            }
        }

        private void buttonOrderByDesc_Click(object sender, EventArgs e)
        {
            listViewOrdCol.Clear();
            var OrdCol = ColOfPeople.OrderByDescending(a => a.Age);
            foreach (Person item in OrdCol)
            {
                listViewOrdCol.Items.Add(item.ToString());
            }
        }

        private void buttonLINQ1_Click(object sender, EventArgs e)
        {
            listViewLINQ1.Clear();
            var OrdCol = ColOfPeople.Where(a => a.Age<40);
            foreach (Person item in OrdCol)
            {
                listViewLINQ1.Items.Add(item.ToString());
            }
        }

        private void buttonLINQ2_Click(object sender, EventArgs e)
        {
            listViewLINQ2.Clear();
            var OrdCol = ColOfPeople.Where(a => a.Name.Length<5);
            foreach (Person item in OrdCol)
            {
                listViewLINQ2.Items.Add(item.ToString());
            }
        }

        private void buttonLINQ3_Click(object sender, EventArgs e)
        {
            Letter = textBox1.Text;
            listViewLINQ3.Clear();
            if (!String.IsNullOrEmpty(Letter))
            {
                var OrdCol = ColOfPeople.Where(a => a.Name.StartsWith(Letter));
                foreach (Person item in OrdCol)
                {
                    listViewLINQ3.Items.Add(item.ToString());
                }
            }
        }
    }
}
