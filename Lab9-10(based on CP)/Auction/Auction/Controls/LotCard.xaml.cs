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
using Auction.DataAccess.UnitOfWork;
using MaterialDesignThemes.Wpf;

namespace Auction.Controls
{
    /// <summary>
    /// Interaction logic for LotCard.xaml
    /// </summary>
    public partial class LotCard : UserControl
    {
        private bool _isDeleted;
        public LotCard()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => _title.Text;
            set => _title.Text = value;
        }

        public string Description
        {
            get => _desc.Text;
            set => _desc.Text = value;
        }

        public ImageSource ImageSource
        {
            get => _image.Source;
            set => _image.Source = value;
        }

        public event EventHandler<EventArgs> OnDataUpdated;
        public event EventHandler<EventArgs> OnDataRequestUpdate;

        public int LotId { get; set; }

        public decimal StratBid { get; set; }

        private void Delete_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            using (var uow = new AuctionUnitOfWork(true))
            {
                var lot = uow.LotRepository.Get(LotId);
                lot.Users.Clear();
                uow.LotRepository.Delete(lot);

                uow.Commit();
            }

            OnOnDataUpdated();

            _isDeleted = true;
        }

        protected virtual void OnOnDataUpdated()
        {
            if (!_isDeleted)
                OnDataUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void Edit_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            OnDataRequestUpdate?.Invoke(LotId, EventArgs.Empty);
        }
    }
}
