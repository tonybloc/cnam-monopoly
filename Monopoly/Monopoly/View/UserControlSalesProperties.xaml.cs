using Monopoly.Handlers;
using Monopoly.Models.Bank;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Cards;
using Monopoly.Models.Components.Cells;
using Monopoly.View.Notifications.Dialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Monopoly.View
{
    /// <summary>
    /// Logique d'interaction pour UserControlSalesProperties.xaml
    /// </summary>
    public partial class UserControlSalesProperties : UserControl, INotifyPropertyChanged
    {

        private PlayerHandler _PlayerHandler;
        private Player CurrentPlayer;
        private double AmountToPay;
        private Card CardTempo;

        private Property _PropertySelected;
        public Property PropertySelected
        {
            get { return _PropertySelected; }
            set
            {
                if(_PropertySelected != value)
                {
                    _PropertySelected = value;
                }
            }
        }

        private int _indexSelected;
        public int IndexSelected
        {
            get
            {
                return _indexSelected;
            }
            set
            {
                if (_indexSelected != value)
                {
                    _indexSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _PropertyInMortgage;
        public bool PropertyInMortgage
        {
            get { return _PropertyInMortgage; }
            set
            {
                if(_PropertyInMortgage != value)
                {
                    _PropertyInMortgage = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _btnMortgageEnable;
        private bool _btnSellHouseEnable;
        private bool _btnSellHotelEnable;
        private bool _btnExchange;

        public bool BtnMortgageEnable
        {
            get { return _btnMortgageEnable; }
            set
            {
                if(_btnMortgageEnable != value)
                {
                    _btnMortgageEnable = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool BtnSellHouseEnable
        {
            get { return _btnSellHouseEnable; }
            set
            {
                if (_btnSellHouseEnable != value)
                {
                    _btnSellHouseEnable = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool BtnSellHotelEnable
        {
            get { return _btnSellHotelEnable; }
            set
            {
                if (_btnSellHotelEnable != value)
                {
                    _btnSellHotelEnable = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool BtnExchangeEnable
        {
            get { return _btnExchange; }
            set
            {
                if (_btnExchange != value)
                {
                    _btnExchange = value;
                    OnPropertyChanged();
                }
            }
        }

        public delegate void EventDoCellAction();
        public static event EventDoCellAction EventCellAction;

        public delegate void EventDoCardAction(Card c);
        public static event EventDoCardAction EventCardAction;

        public ObservableCollection<Property> ListOfProperties { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructeur
        
        /// <summary>
        /// Create new instance of class
        /// </summary>
        /// <param name="amount">Amount to pay</param>
        public UserControlSalesProperties(double amount)
        {
            // Initialize UI Component
            InitializeComponent();

            // Context
            this.DataContext = this;
            
            // Handler
            _PlayerHandler = PlayerHandler.Instance;

            // Event
            PlayerHandler.EventNotifyAlertMessage += UINotifyAlertMessage;

            // Variable Initialisation
            CardTempo = null;
            IndexSelected = -1;
            AmountToPay = amount;
            CurrentPlayer = _PlayerHandler.currentPlayer;
            ListOfProperties = CurrentPlayer.ListOfProperties;
            BtnExchangeEnable = true;
            BtnMortgageEnable = false;
            BtnSellHotelEnable = false;
            BtnSellHouseEnable = false;
            PropertyInMortgage = false;
        }

        /// <summary>
        /// Create new instance of class
        /// </summary>
        /// <param name="amount">amount</param>
        /// <param name="c">card</param>
        public UserControlSalesProperties(double amount, Card c)
        {
            // Initialize UI Component
            InitializeComponent();

            // Context
            this.DataContext = this;

            // Handler
            _PlayerHandler = PlayerHandler.Instance;

            // Event
            PlayerHandler.EventNotifyAlertMessage += UINotifyAlertMessage;

            // Variable Initialisation
            CardTempo = c;
            IndexSelected = -1;
            AmountToPay = amount;
            CurrentPlayer = _PlayerHandler.currentPlayer;
            ListOfProperties = CurrentPlayer.ListOfProperties;
            BtnExchangeEnable = true;
            BtnMortgageEnable = false;
            BtnSellHotelEnable = false;
            BtnSellHouseEnable = false;
            PropertyInMortgage = false;
        }
        #endregion

        // Event declared : Property Changed
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Events

        public void OnClick_GiveUp(object sender, EventArgs e)
        {
            _PlayerHandler.PlayerGiveUp(CurrentPlayer);
            this.Content = null;
        }

        public void OnClick_Continu(object sender, EventArgs e)
        {
            if(_PlayerHandler.CheckIfPlayerCanBuy(CurrentPlayer, AmountToPay))
            {
                this.Content = null;
                if(CardTempo == null)
                {
                    EventCellAction();
                }
                else
                {
                    EventCardAction(CardTempo);
                }
            }
            else
            {
                UINotifyAlertMessage("Vous n'avez toujours pas assez d'argent pour payer le montant : " + AmountToPay, AlertDialog.TypeOfAlert.WARNING);
            }
        }


        public void OnClick_Mortgage(object sender, EventArgs e)
        {
            _PlayerHandler.Mortgage(CurrentPlayer, PropertySelected);
            BtnMortgageEnable = false;
        }
        public void OnClick_SellHotel(object sender, EventArgs e)
        {
            _PlayerHandler.Sell(CurrentPlayer, (Land)PropertySelected);
            BtnSellHotelEnable = (((Land)PropertySelected).NbHotel != 0);
            BtnSellHouseEnable = (((Land)PropertySelected).NbHouse != 0);
        }

        public void OnClick_SellHouse(object sender, EventArgs e)
        {
            _PlayerHandler.Sell(CurrentPlayer, (Land)PropertySelected);
            BtnSellHotelEnable = (((Land)PropertySelected).NbHotel != 0);
            BtnSellHouseEnable = (((Land)PropertySelected).NbHouse != 0);
        }

        public void OnClick_Exchange(object sender, EventArgs e)
        {
            this.Content = null;
        }
        
        public void onSelecionChangeListeViewProperties(object sender, EventArgs e)
        {
            if(PropertySelected is Land)
            {

                PropertyInMortgage = (PropertySelected.Status == Property.MORTGAGED);
                BtnExchangeEnable = true;
                BtnMortgageEnable = !PropertyInMortgage;
                BtnSellHotelEnable = (((Land)PropertySelected).NbHotel != 0);
                BtnSellHouseEnable = (((Land)PropertySelected).NbHouse != 0);
            }
            else if(PropertySelected is TrainStation)
            {
                BtnExchangeEnable = true;
                BtnMortgageEnable = !PropertyInMortgage;
                BtnSellHotelEnable = false;
                BtnSellHouseEnable = false;
            }
            else if (PropertySelected is PublicService)
            {
                BtnExchangeEnable = true;
                BtnMortgageEnable = !PropertyInMortgage;
                BtnSellHotelEnable = false;
                BtnSellHouseEnable = false;
            }
            else
            {
                BtnExchangeEnable = true;
                BtnMortgageEnable = false;
                BtnSellHotelEnable = false;
                BtnSellHouseEnable = false;

            }
        }

        #endregion

        #region UI Notification
        /// <summary>
        /// Display an alert
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        private void UINotifyAlertMessage(string message, AlertDialog.TypeOfAlert type)
        {
            AlertNotification.Content = new AlertDialog(message, type);
            AlertNotification.Visibility = Visibility.Visible;
        }
        #endregion

    }
}
