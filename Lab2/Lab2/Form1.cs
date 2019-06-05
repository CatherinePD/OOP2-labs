using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Дисциплина : Form
    {
        [Serializable]
        public class Discipline
        {
            public string Name { get; set; }
            public int Semester { get; set; }
            public int Course { get; set; }
            public List<string> Specialty;
            public int NumOfLectures { get; set; }
            public int NumOfLabs { get; set; }
            public string TypeOfControl { get; set; }
            public Lector lector { get; set; }
            public Discipline(Lector lect)
            {
                lector = lect;
                Specialty = new List<string>();
            }
        }
        
        private Lector lector;
        private Discipline discipline;
        private JsonSerializer<Discipline> _jsonSerializer;

        public Дисциплина()
        {
            InitializeComponent();
            lector = new Lector();
            discipline = new Discipline(lector);
            _jsonSerializer = new JsonSerializer<Discipline>("data.json");
            comboBox1.SelectedIndex = 0;

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Lector = discipline.lector;
            form.ShowDialog();
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
                return false;
            if (checkedListBox1.CheckedItems.Count < 1)
                return false;
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Validate())
            {
                MessageBox.Show("Заполните все поля необходимой информацией!");
                return;
            }
            if (String.IsNullOrEmpty(lector.Name))
            {
                MessageBox.Show("Добавьте информацию о лекторе!");
                return;
            }
            discipline.Name = textBox1.Text;
            discipline.Course = Int32.Parse(comboBox1.SelectedItem.ToString());
            if (radioButton1.Checked)
                discipline.Semester = Int32.Parse(radioButton1.Text);
            else if (radioButton2.Checked)
                discipline.Semester = Int32.Parse(radioButton2.Text);
            discipline.Specialty = checkedListBox1.CheckedItems.Cast<string>().ToList();
            discipline.NumOfLectures = (int)numericUpDown1.Value;
            discipline.NumOfLabs = trackBar1.Value;
            if (radioButton3.Checked)
                discipline.TypeOfControl = radioButton3.Text;
            else if (radioButton4.Checked)
                discipline.TypeOfControl = radioButton3.Text;

            _jsonSerializer.Serialize(discipline);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = trackBar1.Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Clear();
                var info = _jsonSerializer.Deserialize();
                richTextBox1.Text = $"Название дисциплины: {info.Name} \nВедётся на {info.Course} курсе \nВ(о) {info.Semester} семетре \n" +
                    $"У специальности(ей) {String.Join(", ",info.Specialty)} \nКоличество лекций: {info.NumOfLectures} \nКоличество лабораторных {info.NumOfLabs} \n" +
                    $"Вид контроля: {info.TypeOfControl}\nИнформация о лекторе:\nФамилия: {info.lector.Surname}\nИмя: {info.lector.Name} " +
                    $"\nОтчество: {info.lector.Patronomyc} \nКафедра: {info.lector.Department} \nАудитория: {info.lector.Auditory} \nКонтактный номер: {info.lector.Number}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Информация не была сохранена");
            }

        }
    }
    [Serializable]
    public class Lector
    {
        public string Name;
        public string Surname;
        public string Patronomyc;
        public string Department;
        public string Auditory;
        public string Number;
    }
}
