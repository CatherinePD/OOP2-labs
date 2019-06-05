using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right; //cправа налево текст
            
        }
         class Operand
        {
            public string Text { get; set; }
            public int Value
            {
                get { return int.Parse(Text); }
            }

        }
        private Operand firstParam = new Operand();
        private Operand secondParam = new Operand();
        private Func<int, int, int> Operation;
        private string memory;

        private void Calc()
        {
            if (firstParam.Text != null && secondParam.Text != null)
            {
                try
                {
                firstParam.Text = Operation(firstParam.Value, secondParam.Value).ToString(); 
                secondParam.Text = null;
                richTextBox1.Text = firstParam.Text;
                }
                catch(DivideByZeroException e)
                {
                    richTextBox1.Clear();
                    MessageBox.Show("It is impossible to divide by zero");
                    firstParam.Text = null;
                    secondParam.Text = null;
                }
                catch (NullReferenceException e)
                {
                    richTextBox1.Clear();
                    MessageBox.Show("Error");
                    firstParam.Text = null;
                    secondParam.Text = null;
                }
            }
            Operation = null;
        }
        
        private void Clicks(object sender, EventArgs e)
        {
            richTextBox1.Text += ((Button)sender).Text;

            if (Operation == null)
                firstParam.Text = richTextBox1.Text;
            else
                secondParam.Text = richTextBox1.Text;
        }
        
        private void buttonEquals_Click(object sender, EventArgs e)
        {
            Calc();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            memory = richTextBox1.Text;
        }

        private void buttonExtract_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = memory;

            if (Operation == null)
                firstParam.Text = richTextBox1.Text;
            else
                secondParam.Text = richTextBox1.Text;
        }
        
        private void buttonMod_Click(object sender, EventArgs e)
        {
            if (Operation != null)
            {
                Calc();
            }
            Operation = (x, y) => x % y; 
            richTextBox1.Clear();
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            
            if (Operation != null)
            {
                Calc();
            }
            Operation = (x, y) => x + y;
            richTextBox1.Clear();
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            if (Operation != null)
            {
                Calc();
            }
            Operation = (x, y) => x / y;
            richTextBox1.Clear();
        }


        private void buttonMult_Click(object sender, EventArgs e)
        {
            if (Operation != null)
            {
                Calc();
            }
            Operation = (x, y) => x * y; ;
            richTextBox1.Clear();
        }
        

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (Operation != null)
            {
                Calc();
            }
            Operation = (x, y) => x - y;
            richTextBox1.Clear();
            if (firstParam.Text == null)
            {
                richTextBox1.Text = "-";
                Operation = null;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            Operation = null;
            firstParam.Text = null;
            secondParam.Text = null;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(richTextBox1.Text))
              richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.Text.Length - 1, 1);
            if (Operation == null)
                firstParam.Text = richTextBox1.Text;
            else
                secondParam.Text = richTextBox1.Text;
        }
    }
}
