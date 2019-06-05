using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        private Lector lector;
        private Discipline discipline;
        private XmlSerializer<List<Discipline>> _xmlSerializer;

        public Дисциплина()
        {
            InitializeComponent();
            lector = new Lector();
            discipline = new Discipline(lector);
            _xmlSerializer = new XmlSerializer<List<Discipline>>("data.xml");
            comboBox1.SelectedIndex = 0;

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Lector = discipline.lector;
            form.ShowDialog();
        }

        private bool Validate(Discipline model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            if (!Validator.TryValidateObject(model, context, results, true))
            {
                string strWithErrroe = "";
                foreach (var error in results)
                    strWithErrroe += $"{error.ErrorMessage}\n";
                
                MessageBox.Show(strWithErrroe);
            }
            return !results.Any();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var model = new Discipline();
            model.Name = textBox1.Text;
            model.Course = Int32.Parse(comboBox1.SelectedItem.ToString());
            if (radioButton1.Checked)
                model.Semester = Int32.Parse(radioButton1.Text);
            else if (radioButton2.Checked)
                model.Semester = Int32.Parse(radioButton2.Text);
            model.Specialty = checkedListBox1.CheckedItems.Cast<string>().ToList();
            model.NumOfLectures = (int)numericUpDown1.Value;
            model.NumOfLabs = trackBar1.Value;
            if (radioButton3.Checked)
                model.TypeOfControl = radioButton3.Text;
            else if (radioButton4.Checked)
                model.TypeOfControl = radioButton3.Text;
            model.lector = discipline.lector;

            if (!Validate(model))
                return;

            discipline = model;
            
            var currentList = GetCurrentList();

            currentList.Add(discipline);

            _xmlSerializer.Serialize(currentList);
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
                var currentList = _xmlSerializer.Deserialize();

                var info = currentList.Last();

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

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Disciplines = GetCurrentList();
            form.ShowDialog();
        }

        private void поКоличествуЛекцийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortAndSave("sortByLectures.xml", a => a.NumOfLectures);
        }

        private void поВидуКонтроляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortAndSave("sortByControl.xml", a => a.TypeOfControl);
        }

        private void SortAndSave<T>(string fileName, Func<Discipline, T> keySelector)
        {
            var currentList = GetCurrentList();

            var sorted = currentList.OrderBy(keySelector).ToList();

            _xmlSerializer.Serialize(sorted, fileName);
            MessageBox.Show("Отсортировано. Смотрите xml-файл");
        }

        private List<Discipline> GetCurrentList()
        {
            return _xmlSerializer.Deserialize() ?? new List<Discipline>();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Разработчик Петрович Екатерина Дмитриевна\nВерсия приложения 1.0\nВсе права защищены БГТУ 2019");
        }
    }
    
}
