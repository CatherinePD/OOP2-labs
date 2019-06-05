using System;
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
using Auction.DataAccess;
using Auction.DataAccess.Entities;
using Auction.DataAccess.UnitOfWork;

namespace Auction
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> _products;
        private Lot _currentLot;
        private int _userId;

        public MainWindow()
        {
            InitializeComponent();

            using (var uow = new AuctionUnitOfWork())
            {
                var user = uow.UserRepository.Get().FirstOrDefault();
                _userId = user.Id;
            }
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)//add tovar
        {
            using (var uow = new AuctionUnitOfWork())
            {
                var category = uow.CategoryRepository.Get().FirstOrDefault();
                var content = uow.ProductContentRepository.Get().FirstOrDefault();
                var user = uow.UserRepository.Get(_userId);

                var product = new Product
                {
                    Name = ProductTextBox.Text,
                    Category = category,
                    CategoryId = category.Id,
                    Owner = user,
                    OwnerId = user.Id,
                    ProductContents = new List<ProductContent> { content }
                };

                var nameArg = new System.Data.SqlClient.SqlParameter("name", product.Name);
                var categoryArg = new System.Data.SqlClient.SqlParameter("category", category.Id);
                var userArg = new System.Data.SqlClient.SqlParameter("user", user.Id);
                await uow.ProductRepository.Query(
                    "INSERT INTO dbo.Products (Name, CategoryId, OwnerId) VALUES(@name, @category, @user)",
                    nameArg, categoryArg, userArg);

                //_products.Add(product);

                ReloadProducts();

                ProductTextBox.Clear();
            }
        }

        private void ReloadProducts()//загрузить в комбобокс из базы
        {
            ProductComboBox.ItemsSource = null;

            using (var uow = new AuctionUnitOfWork())
            {
                _products = uow.ProductRepository.Get(p => p.OwnerId == _userId).ToList();
            }

            ProductComboBox.ItemsSource = _products;
            ProductComboBox.DisplayMemberPath = "Name";
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ReloadProducts();
        }

        private void AddLot_OnClick(object sender, RoutedEventArgs e)// Добавить лот в базу и карточки и отсортировать карточки
        {
            using (var uow = new AuctionUnitOfWork(true)) 
            {
                var user = uow.UserRepository.Get(_userId);
                var selectedProduct = (Product)ProductComboBox.SelectedItem;
                var product = uow.ProductRepository.Get(selectedProduct.Id);

                var lot = new Lot
                {
                    Title = TitleTextBox.Text,
                    StartBid = decimal.Parse(StartBidTextBox.Text),
                    CurrentBid = decimal.Parse(StartBidTextBox.Text),
                    MinBidStep = decimal.Parse(MinStepTextBox.Text),
                    DateCreated = DateTime.Now,
                    DateToExpire = new DateTime(2019, 11, 11),
                    Users = new List<User> {user},
                    Product = product,
                    ProductId = product.Id
                };
                
                uow.LotRepository.Add(lot);

                uow.Commit();
            }

            LotListControl.RefreshAndSort(l => l.Id);
            ClearForm();
        }

        private void LotListControl_OnOnCardUdateRequest(object sender, EventArgs e)
        {
            
            var id = (int) sender;

            using (var uow = new AuctionUnitOfWork()) // Заполнить форму из лота
            {
                var lot = uow.LotRepository.Get(id);

                TitleTextBox.Text = lot.Title;
                StartBidTextBox.Text = lot.StartBid.ToString();
                MinStepTextBox.Text = lot.MinBidStep.ToString();
                ProductComboBox.SelectedItem = _products.FirstOrDefault(p => p.Id == lot.ProductId);

                _currentLot = lot;
            }

            EditLot.IsEnabled = true;
        }

        private void EditLot_OnClick(object sender, RoutedEventArgs e)
        {
            using (var uow = new AuctionUnitOfWork(true)) // Обновить лот
            {
                var lot = uow.LotRepository.Get(_currentLot.Id);
                var selectedProduct = (Product)ProductComboBox.SelectedItem;
                var product = uow.ProductRepository.Get(selectedProduct.Id);

                lot.Title = TitleTextBox.Text;
                lot.StartBid = decimal.Parse(StartBidTextBox.Text);
                lot.MinBidStep = decimal.Parse(MinStepTextBox.Text);
                lot.ProductId = product.Id;
                lot.Product = product;

                uow.LotRepository.Update(lot);
                uow.Commit();
            }
            LotListControl.RefreshAndSort(l => l.Id);
            ClearForm();
        }

        private void ClearForm()
        {
            TitleTextBox.Clear();
            StartBidTextBox.Clear();
            MinStepTextBox.Clear();
            ProductComboBox.SelectedItem = null;

            EditLot.IsEnabled = false;
        }

        private void SortByTitle_OnClick(object sender, RoutedEventArgs e)
        {
            LotListControl.RefreshAndSort(l => l.Title);
        }

        private void SortByBid_OnClick(object sender, RoutedEventArgs e)
        {
            LotListControl.RefreshAndSort(l => l.StartBid);
        }

        private void SearchButton_OnClick(object sender, RoutedEventArgs e)
        {
            LotListControl.Find(SearchTextBox.Text);
        }
    }
}
