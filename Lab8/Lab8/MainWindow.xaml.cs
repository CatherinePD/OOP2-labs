using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Lab8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        SqlConnection _connection;       // для создания канала связи между программой и источником данных 
        SqlCommand _command;
        SqlDataAdapter _adapter1;        // для заполнения DataSet (образ бд) и обновления БД 
        SqlDataAdapter _adapter2;
        DataTable _dataDiscTable;             // таблица бд
        DataTable _dataLectTable;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();

                string sqlExpression = "SELECT * FROM Disciplines";
                _dataDiscTable = new DataTable();
                _command = new SqlCommand(sqlExpression, _connection);
                _adapter1 = new SqlDataAdapter(_command);
                _adapter1.Fill(_dataDiscTable);
                dataGridDisc.ItemsSource = _dataDiscTable.DefaultView; //для редактирования

                string sqlExpression2 = "SELECT * FROM Lectors";
                _dataLectTable = new DataTable();
                _command = new SqlCommand(sqlExpression2, _connection);
                _adapter2 = new SqlDataAdapter(_command);
                _adapter2.Fill(_dataLectTable);
                dataGridLect.ItemsSource = _dataLectTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(_adapter1);  // автоматич генер-т команды, кот позволяют согласовать
            SqlCommandBuilder comandbuilder2 = new SqlCommandBuilder(_adapter2); // изменения, вносимые в объект dataset, со связанной БД
            _adapter1.Update(_dataDiscTable); //обновляем значения в БД
            _adapter2.Update(_dataLectTable);
            MessageBox.Show("Информация в базе данных обновлена");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLect.SelectedItem != null)
            {
                var selected = (DataRowView)dataGridLect.SelectedItem;
                Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
                if (openFileDlg.ShowDialog() == true)
                    selected.Row[7] = openFileDlg.FileName;
            }
            else
            {
                MessageBox.Show("Необходимо выделить строку с нужным лектором для добавления ему фото");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridDisc.SelectedItems != null)
            {
                for (int i = 0; i < dataGridDisc.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = (DataRowView)dataGridDisc.SelectedItems[i];
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            if (dataGridLect.SelectedItems != null)
            {
                for (int i = 0; i < dataGridLect.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = (DataRowView)dataGridLect.SelectedItems[i];
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            
        }

        private void Proc_Click(object sender, RoutedEventArgs e)
        {
            string sqlExpression = "sp_TestRow";
            SqlTransaction tran = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                tran = connection.BeginTransaction();
                command.Transaction = tran;

                command.ExecuteNonQuery(); //выполн запрос и возвр кол-во задейств в инстр строк

                tran.Commit(); //фиксирует транзакцию бд

                Window_Loaded(new object(), new RoutedEventArgs());
            }
        }
    }
}
