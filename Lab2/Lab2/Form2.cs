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
        private bool Validate()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
                return false;
            if (string.IsNullOrEmpty(textBox2.Text))
                return false;
            if (string.IsNullOrEmpty(textBox3.Text))
                return false;
            if (string.IsNullOrEmpty(textBox4.Text))
                return false;
            if (string.IsNullOrEmpty(maskedTextBox1.Text))
                return false;
            if (maskedTextBox1.Text.Length != 17)
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
            Lector.Name = textBox1.Text;
            Lector.Surname = textBox2.Text;
            Lector.Patronomyc = textBox3.Text;
            Lector.Department = comboBox1.SelectedItem.ToString();
            Lector.Auditory = textBox4.Text;
            Lector.Number = maskedTextBox1.Text;
            this.Close();
        }
    }
}
