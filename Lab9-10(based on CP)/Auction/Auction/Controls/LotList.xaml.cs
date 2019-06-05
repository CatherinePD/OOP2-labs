using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
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
using Auction.DataAccess.Entities;
using Auction.DataAccess.UnitOfWork;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;

namespace Auction.Controls
{
    /// <summary>
    /// Логика взаимодействия для LotList.xaml
    /// </summary>
    public partial class LotList : UserControl
    {
        public LotList()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs> OnCardUdateRequest;

        public void RefreshAndSort<T>(Func<Lot, T> keySelector)
        {
            using (var uow = new AuctionUnitOfWork())
            {
                var lots = GetLots(uow, keySelector);
                LoadCards(lots);
            }
        }

        public void Find(string text)
        {
            using (var uow = new AuctionUnitOfWork())
            {
                var lots = uow.LotRepository.Get(l => l.Title.Contains(text)).ToList();

                LoadCards(lots);
            }
        }

        private void LoadCards(List<Lot> lots)
        {
            lotStackPanel.Children.Clear();

            lots.ForEach(lot =>
            {
                var user = lot.Product.Owner;
                var content = lot.Product.ProductContents.FirstOrDefault() ?? new ProductContent { Content = "../Resources/tree1.png" };

                var card = new LotCard
                {
                    Title = lot.Title,
                    LotId = lot.Id,
                    Height = 136,
                    Description = $"{lot.StartBid.ToString()} by {user.Login}",
                    StratBid = lot.StartBid,
                    ImageSource = new BitmapImage(new Uri(content.Content, UriKind.Relative))
                };

                card.OnDataUpdated += CardOnOnDataUpdated; //обновить список лотов
                card.OnDataRequestUpdate += CardOnOnDataRequestUpdate; //заполнить форму на window

                lotStackPanel.Children.Add(card);
            });
        }

        private void CardOnOnDataRequestUpdate(object sender, EventArgs eventArgs)
        {
            OnCardUdateRequest?.Invoke(sender, eventArgs);
        }

        private void CardOnOnDataUpdated(object sender, EventArgs eventArgs)
        {
            RefreshAndSort(l => l.Id);
        }
        private void Loaded_Cards(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            RefreshAndSort(l => l.Id);
        }

        private List<Lot> GetLots<T>(AuctionUnitOfWork uow, Func<Lot, T> keySelector)
        {
            return uow.LotRepository.GetSorted(keySelector).ToList();
        }

        private void Card_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
