using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public List<Discipline> Disciplines { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            IEnumerable<Discipline> result = new List<Discipline>(Disciplines);

            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                var regex = new Regex(@"^" + textBox1.Text + "(\\w*)", RegexOptions.IgnoreCase);

                result = result.Where(d => regex.IsMatch(d.lector.Surname)).ToList();
            }

            if (comboBox1.SelectedIndex > -1)
            {
                result = result.Where(d => d.Semester == int.Parse(comboBox1.SelectedItem.ToString()));
            }

            if (comboBox2.SelectedIndex > -1)
            {
                result = result.Where(d => d.Course == int.Parse(comboBox2.SelectedItem.ToString()));
            }

            foreach (var item in result)
                listBox1.Items.Add($"Дисциплина: {item.Name}, Лектор: {item.lector.Surname} {item.lector.Name} {item.lector.Patronomyc}," +
                    $"Курс: {item.Course}, Семестр: {item.Semester}, Вид контроля: {item.TypeOfControl}");

            var serializer = new XmlSerializer<List<Discipline>>("searchResults.xml");
            serializer.Serialize(result.ToList());
        }
    }
}
