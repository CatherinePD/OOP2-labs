using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace Lab4_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FontCollection = Fonts.SystemFontFamilies.ToList();

            fonts.ItemsSource = FontCollection;
            fonts.DisplayMemberPath = "Source";
            fonts.SelectedValuePath = "Source";
            fonts.SelectedValue = richtextbox.FontFamily.Source;
            fontsMenu.ItemsSource = FontCollection;

            fontSize.SelectedValue = richtextbox.FontSize;
            fontColor.SelectedIndex = 0;
            fontColor2.SelectedIndex = 1;

            this.richtextbox.AddHandler(RichTextBox.DragOverEvent, new DragEventHandler(this.DragItem), true);
            this.richtextbox.AddHandler(RichTextBox.DropEvent, new DragEventHandler(this.DropItem), true);
        }

        public List<FontFamily> FontCollection { get; set; }

        private void ToolBar_SelectionChanged(object sender, SelectionChangedEventArgs e)//ф-ия тулбара где шрифты 
        {
            ComboBox source = e.OriginalSource as ComboBox;
            if (source == null) return;

            switch (source.Name)
            {
                case "fonts"://шрифты
                    ApplySelection(TextBlock.FontFamilyProperty, source.SelectedItem);//

                    break;
                case "fontSize"://размер
                    ApplySelection(TextBlock.FontSizeProperty, source.SelectedItem);
                    break;
                case "fontColor"://цвет букв
                    ApplySelection(TextBlock.ForegroundProperty, source.SelectedItem);
                    break;
                case "fontColor2"://фон
                    ApplySelection(TextBlock.BackgroundProperty, source.SelectedItem);
                    break;
            }

        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem source = e.OriginalSource as MenuItem;
            MenuItem sourceparent = e.Source as MenuItem;
            if (source == null) return;

            switch (sourceparent.Name)
            {
                case "fontsMenu"://шрифты
                    ApplySelection(TextBlock.FontFamilyProperty, source.Header);//
                    fonts.SelectedValue = source.Header;
                    break;
                case "fontSizeMenu"://размер
                    ApplySelection(TextBlock.FontSizeProperty, source.Header);
                    fontSize.SelectedValue = source.Header;
                    break;
                case "fontColorMenu"://цвет букв
                    ApplySelection(TextBlock.ForegroundProperty, source.Header);
                    fontColor.SelectedValue = source.Header;
                    break;
                case "fontColor2Menu"://фон
                    ApplySelection(TextBlock.BackgroundProperty, source.Header);
                    fontColor2.SelectedValue = source.Header;
                    break;
            }
        }
        public void ApplySelection(DependencyProperty property, object value)//для применения выбора
        {
            if (value != null)
            {
                richtextbox.Selection.ApplyPropertyValue(property, value);//применяет свойство к указанному выделению
            }
        }
        private void fontColorAddItems(ItemsControl element)
        {
            element.Items.Add("Black");
            element.Items.Add("White");
            element.Items.Add("Red");
            element.Items.Add("Green");
            element.Items.Add("Blue");
            element.Items.Add("Yellow");
            element.Items.Add("Violet");
            element.Items.Add("Pink");
            element.Items.Add("Gold");
        }
        private void fontSizeAddItems(ItemsControl element)
        {
            for (double i = 8; i < 12; i++)
            {
                element.Items.Add(i);
            }
            for (double i = 12; i <= 28; i += 2)
            {
                element.Items.Add(i);
            }
        }
            private void TextEditLoad(object sender, RoutedEventArgs e)
        {
            fontSizeAddItems(fontSize);
            fontSizeAddItems(fontSizeMenu);
            fontColorAddItems(fontColor);
            fontColorAddItems(fontColor2);
            fontColorAddItems(fontColorMenu);
            fontColorAddItems(fontColor2Menu);

            StreamResourceInfo sri = Application.GetResourceStream(
                new Uri("Arrow.cur", UriKind.Relative));
            Cursor customCursor = new Cursor(sri.Stream);
            this.Cursor = customCursor;
            richtextbox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        }
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            richtextbox.Selection.Text = "";
        }

        private bool IsEmpty(RichTextBox richTextBox)
        {
            string text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text.Replace("\r\n","");
            return string.IsNullOrEmpty(text);
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!IsEmpty(richtextbox))
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Хотите сохранить изменения в файле?", "Сохранение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Save_Executed(sender, e);
                }
                richtextbox.Document.Blocks.Clear();
            }
        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Сохранить файл";
            dialog.Filter = "Текстовый файл (*.txt)|*.txt| Все файлы (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(dialog.FileName, true, Encoding.Default))
                {
                    string richText = new TextRange(richtextbox.Document.ContentStart, richtextbox.Document.ContentEnd).Text;
                    sw.Write(richText);
                }
            }
        }
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application app = Application.Current;
            app.Shutdown();
        }
        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(richtextbox as Visual, "Распечатываемый элемент блокнота");
            }
        }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            New_Executed(sender, e);
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Открыть файл";
            dialog.Filter = "Текстовый файл (*.txt)|*.txt| Все файлы (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                string strfilename = dialog.FileName;
                using (StreamReader sr = new StreamReader(strfilename, Encoding.Default))//считывание с файла
                {
                    string filetext = sr.ReadToEnd();
                    richtextbox.AppendText(filetext);
                }
            }
            this.Title = "CatherineTE (" + dialog.FileName + ") ";
        }
        private int Text_Length(RichTextBox rtb)
        {
            string richText = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text.Replace("\r\n", "");
            int length = richText.Length;
            return length;
        }

        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            label1.Content = $"{Text_Length(richtextbox)}";

        }
        private void DragItem(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void DropItem(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] docPath = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (System.IO.File.Exists(docPath[0]))
                {
                    try
                    {
                        TextRange range = new TextRange(this.richtextbox.Document.ContentStart, this.richtextbox.Document.ContentEnd);
                        FileStream fStream = new FileStream(docPath[0], FileMode.OpenOrCreate);
                        range.Load(fStream, DataFormats.Rtf);
                        fStream.Close();
                        this.Title = "CatherineTE (" + docPath[0] + ") ";
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Файл не может быть открыт, поскольку не является текстовым файлом.");
                    }
                }
            }
        }
        private void SetRussian(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Resources = new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/resources/langRU.xaml")
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void SetEnglish(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Resources = new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/resources/langEN.xaml")
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }

}
