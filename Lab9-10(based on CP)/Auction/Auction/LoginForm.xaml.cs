﻿using System;
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

namespace Auction
{
    /// <summary>
    /// Логика взаимодействия для LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var registration = new Registration();
            this.Close();
            registration.ShowDialog();
        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
