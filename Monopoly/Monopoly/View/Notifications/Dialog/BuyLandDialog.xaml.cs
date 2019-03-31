using Monopoly.Handlers;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Cells;
using Monopoly.Models.Components.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Monopoly.View.Notifications.Dialog
{
    /// <summary>
    /// Logique d'interaction pour BuyLandDialog.xaml
    /// </summary>
    public partial class BuyLandDialog : UserControl, INotifyPropertyChanged
    {
        private PlayerHandler _PlayerHandler;
        private Property _currentProperty;

        public delegate void PropertyBought(Player p);
        public static event PropertyBought propertyBought;

        public delegate void EventExceptionHandling(double amount);
        public static event EventExceptionHandling EventException;


        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Create a new instance of class
        /// </summary>
        /// <param name="exp"></param>
        public BuyLandDialog(Property p) : base()
        {
            InitializeComponent();
            DataContext = this;
            _PlayerHandler = PlayerHandler.Instance;
            Message = "Voulez-vous acheter "+ p.Title + " à " + p.PurchasePrice + " € ?";
            _currentProperty = p;
        }


        private void BtnOui_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _PlayerHandler.BuyProperty(_currentProperty);
                propertyBought(_PlayerHandler.GetCurrentPlayer());
                this.Content = null;

            }
            catch (BankBalanceIsNotEnougth exp)
            {
                this.Content = null;
                EventException(exp.Amount);
            }



        }

        private void BtnNon_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        /// <summary>
        /// Notify the changement an property
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
