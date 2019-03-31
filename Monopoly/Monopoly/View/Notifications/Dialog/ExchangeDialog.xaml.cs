using Monopoly.Handlers;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Cards;
using Monopoly.Models.Components.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour ExchangeDialog.xaml
    /// </summary>
    public partial class ExchangeDialog : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static PlayerHandler _PlayerHandler;

        public ObservableCollection<Player> ListOfPlayers { get; set; }

        // Real lists of current player
        public ObservableCollection<Property> RealListOfProperties { get; set; }
        public ObservableCollection<Card> RealListOfCards { get; set; }

        // Real lists of selected player
        public ObservableCollection<Property> RealListOfPropertiesOtherPlayer { get; set; }
        public ObservableCollection<Card> RealListOfCardsOtherPlayer { get; set; }      

        // lists of current player displayed and filtered
        public ObservableCollection<Property> ListOfProperties { get; set; }
        public ObservableCollection<Card> ListOfCards { get; set; }

        // lists of selected player displayed
        public ObservableCollection<Property> ListOfPropertiesOtherPlayer { get; set; }
        public ObservableCollection<Card> ListOfCardsOtherPlayer { get; set; }

        private Player _selectedPlayer;
        public Player SelectedPlayer
        {
            get
            {
                return _selectedPlayer;
            }
            set
            {
                if (_selectedPlayer != value)
                {
                    _selectedPlayer = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _amountPlayer;
        public int AmountPlayer
        {
            get
            {
                return _amountPlayer;
            }
            set
            {
                if (_amountPlayer != value)
                {
                    _amountPlayer = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _amountOtherPlayer;
        public int AmountOtherPlayer
        {
            get
            {
                return _amountOtherPlayer;
            }
            set
            {
                if (_amountOtherPlayer != value)
                {
                    _amountOtherPlayer = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _canExchange { get; set; }
        public bool CanExchange
        {
            get { return _canExchange; }
            set
            {
                if (_canExchange != value)
                {
                    _canExchange = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _canEditAmount { get; set; }
        public bool CanEditAmount
        {
            get { return _canEditAmount; }
            set
            {
                if (_canEditAmount != value)
                {
                    _canEditAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _canEditAmountOther { get; set; }
        public bool CanEditAmountOther
        {
            get { return _canEditAmountOther; }
            set
            {
                if (_canEditAmountOther != value)
                {
                    _canEditAmountOther = value;
                    OnPropertyChanged();
                }
            }
        }

        private Player _currentPlayer;
        public Player CurrentPlayer
        {
            get
            {
                return _currentPlayer;
            }
            set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ExchangeDialog()
        {
            InitializeComponent();
            DataContext = this;

            _PlayerHandler = PlayerHandler.Instance;
            CurrentPlayer = _PlayerHandler.GetCurrentPlayer();

            RealListOfProperties = CurrentPlayer.ListOfProperties;
            RealListOfCards = CurrentPlayer.ListOfCards;

            ListOfPlayers = new ObservableCollection<Player>(_PlayerHandler.ListOfPlayers.Where(p => p.Id != CurrentPlayer.Id));

            List<Property> properties = CurrentPlayer.ListOfProperties.Where(l => l.Status == Property.NOT_AVAILABLE_ON_SALE).ToList();
            List<Property> ListOtherProperties = properties.Where(l => l.GetType() != typeof(Land)).ToList();
            List<Property> ListOfLands = properties.Where(l => l is Land).Cast<Land>().Where(l => l.NbHouse == 0 && l.NbHotel == 0).OrderBy(l => l.LandGroup.IdGroup).Cast<Property>().ToList();

            var res = new ObservableCollection<Property>();
            ListOtherProperties.ForEach(p => res.Add(p));
            ListOfLands.ForEach(p => res.Add(p));
            res.OrderBy(p => p.Id);

            ListOfProperties = res;
            ListOfCards = CurrentPlayer.ListOfCards;

            ListOfPropertiesOtherPlayer = new ObservableCollection<Property>();
            ListOfCardsOtherPlayer = new ObservableCollection<Card>();
            
            CanExchange = false;

        }

        private void onClickExchange(object sender, RoutedEventArgs e)
        {
            List<Property> listProperties = new List<Property>();
            List<Property> listPropertiesOther = new List<Property>();

            List<Card> listCards = new List<Card>();
            List<Card> listCardsOther = new List<Card>();

            if ((CurrentPlayer.Amount.Amount - AmountPlayer < 0) || (SelectedPlayer.Amount.Amount - AmountOtherPlayer < 0))
            {
                if (CurrentPlayer.Amount.Amount - AmountPlayer < 0)
                    lblAmount.Visibility = Visibility.Visible;

                if (SelectedPlayer.Amount.Amount - AmountOtherPlayer < 0)
                    lblAmountOther.Visibility = Visibility.Visible;

            }
            else
            {
                lblAmount.Visibility = Visibility.Hidden;
                lblAmountOther.Visibility = Visibility.Hidden;

                RealListOfPropertiesOtherPlayer = SelectedPlayer.ListOfProperties;
                RealListOfCardsOtherPlayer = SelectedPlayer.ListOfCards;

                if (lstProperties.Items.Count > 0 && lstProperties.SelectedItems != null)
                {
                    foreach (var item in lstProperties.SelectedItems)
                    {
                        listProperties.Add((Property)item);
                    }

                    foreach (var item in listProperties)
                    {
                        RealListOfProperties.Remove((Property)item);
                        RealListOfPropertiesOtherPlayer.Add((Property)item);
                    }
                }

                if (lstPropertiesOther.Items.Count > 0 && lstPropertiesOther.SelectedItems != null)
                {
                    foreach (var item in lstPropertiesOther.SelectedItems)
                    {
                        listPropertiesOther.Add((Property)item);
                    }

                    foreach (var item in listPropertiesOther)
                    {
                        RealListOfPropertiesOtherPlayer.Remove((Property)item);
                        RealListOfProperties.Add((Property)item);
                    }
                }

                if (lstCards.Items.Count > 0 && lstCards.SelectedItems != null)
                {
                    foreach (var item in lstCards.SelectedItems)
                    {
                        listCards.Add((Card)item);
                    }

                    foreach (var item in listCards)
                    {
                        RealListOfCards.Remove((Card)item);
                        RealListOfCardsOtherPlayer.Add((Card)item);
                    }
                }

                if (lstCardsOther.Items.Count > 0 && lstCardsOther.SelectedItems != null)
                {
                    foreach (var item in lstCardsOther.SelectedItems)
                    {
                        listCardsOther.Add((Card)item);
                    }

                    foreach (var item in listCardsOther)
                    {
                        RealListOfCardsOtherPlayer.Remove((Card)item);
                        RealListOfCards.Add((Card)item);
                    }
                }
                _PlayerHandler.PayeAmount(SelectedPlayer, AmountOtherPlayer);
                _PlayerHandler.PayeAmount(CurrentPlayer, AmountPlayer);
                this.Content = null;
            }

            PageBoard.Instance.RefreshBoardProperties();
        }

        private void onClickCancel(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Property> properties = SelectedPlayer.ListOfProperties.Where(l => l.Status == Property.NOT_AVAILABLE_ON_SALE).ToList();
            List<Property> ListOtherProperties = properties.Where(l => l.GetType() != typeof(Land)).ToList();
            List<Property> ListOfLands = properties.Where(l => l is Land).Cast<Land>().Where(l => l.NbHouse == 0 && l.NbHotel == 0).OrderBy(l => l.LandGroup.IdGroup).Cast<Property>().ToList();

            var res = new ObservableCollection<Property>();
            ListOtherProperties.ForEach(p => res.Add(p));
            ListOfLands.ForEach(p => res.Add(p));
            res.OrderBy(p => p.Id);

            ListOfPropertiesOtherPlayer = res;
            lstPropertiesOther.ItemsSource = ListOfPropertiesOtherPlayer;
            ListOfCardsOtherPlayer = SelectedPlayer.ListOfCards;
            lstCardsOther.ItemsSource = ListOfCardsOtherPlayer;
        }

        private void onListCardsOther_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CanEditAmountOther = true;

            if (CanEditAmountOther == true && CanEditAmount == true)
                CanExchange = true;
        }

        private void onListCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CanEditAmount = true;

            if (CanEditAmountOther == true && CanEditAmount == true)
                CanExchange = true;
        }

        private void onListPropertiesOther_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CanEditAmountOther = true;

            if (CanEditAmountOther == true && CanEditAmount == true)
                CanExchange = true;
        }

        private void onListProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CanEditAmount = true;

            if (CanEditAmountOther == true && CanEditAmount == true)
                CanExchange = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_AmountChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
