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
    public partial class Form2 : Form
    {
        public Lector Lector { get; set; }

        public Form2()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = $"Имя: {textBox1.Text} \nФамилия: {textBox2.Text} \nОтчество: {textBox3.Text} \nКафедра: " +
                $"{comboBox1.SelectedItem.ToString()} \nАудитория: {textBox4.Text} \nКонтактный номер: {maskedTextBox1.Text}";

        }
        private bool Validate(Lector model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            if (!Validator.TryValidateObject(model, context, results, true))
            {
                string strWithErrroe = "";
                foreach (var error in results)
                {
                    strWithErrroe += $"{error.ErrorMessage}\n";
                    
                }
                MessageBox.Show(strWithErrroe);
            }

            return !results.Any();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            var model = new Lector();
            model.Name = textBox1.Text;
            model.Surname = textBox2.Text;
            model.Patronomyc = textBox3.Text;
            model.Department = comboBox1.SelectedItem.ToString();
            model.Auditory = textBox4.Text;
            model.Number = maskedTextBox1.Text;

            if (!Validate(model))
                return;

            Lector.Name = model.Name;
            Lector.Surname = model.Surname;
            Lector.Patronomyc = model.Patronomyc;
            Lector.Department = model.Department;
            Lector.Auditory = model.Auditory;
            Lector.Number = model.Number;
            this.Close();
        }
    }
}
