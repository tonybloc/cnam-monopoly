using Monopoly.Handlers;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Exceptions;
using Monopoly.View.Notifications.Dialog;
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

namespace Monopoly.View
{
    /// <summary>
    /// Logique d'interaction pour PageSinglePlayerCreation.xaml
    /// </summary>
    public partial class PageSinglePlayerCreation : Page, INotifyPropertyChanged
    {
        #region Variables
        private static ColorHandler _ColorHandler;
        private static GameManager _GameManager;
        private static PlayerHandler _PlayerHandler;

        private const string placeholder = "Entrer votre pseudo...";
        public string defaultColorValue = "#FFFFFF";

        private string _playerName;
        public string PlayerName
        {
            get
            {
                return _playerName;
            }
            set
            {
                if (_playerName != value)
                {
                    _playerName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _playerColor;
        public string PlayerColor
        {
            get
            {
                return _playerColor;
            }
            set
            {
                if (_playerColor != value)
                {
                    _playerColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public PageSinglePlayerCreation()
        {
            InitializeComponent();

            int[] quantityBots = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            cbNbBot.ItemsSource = quantityBots;

            DataContext = this;

            _ColorHandler = ColorHandler.Instance;
            _PlayerHandler = PlayerHandler.Instance;
            _GameManager = GameManager.Instance;

            PlayerName = placeholder;
            PlayerColor = defaultColorValue;
        }

        #region NotifyPropertyChanged
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Events        
        private void onGotFocus_Pseudo(object sender, RoutedEventArgs e)
        {
            PlayerName = "";
        }

        private void onLostFocus_Pseudo(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPlayerName.Text))
            {
                PlayerName = placeholder;
            }
        }

        private void onClickPreviousColor(object sender, RoutedEventArgs e)
        {
            PlayerColor = _ColorHandler.GetNextPawnColor();
        }

        private void onClickNextColor(object sender, RoutedEventArgs e)
        {
            PlayerColor = _ColorHandler.GetPreviousPawnColor();
        }

        private void onClickValidate(object sender, RoutedEventArgs e)
        {
            try
            {
                int numberOfPlayerInGame = _PlayerHandler.GetNumberOfPlayer();

                int nbBot = Convert.ToInt32(cbNbBot.SelectedItem.ToString());

                if (PlayerName == placeholder)
                {
                    throw new InvalidePlayerNameException();
                }
                else
                {
                    _PlayerHandler.AddPlayer(new Player(PlayerName, new Pawn(PlayerColor), Player.TypeOfPlayer.USER));
                    _GameManager.GeneratedBot(nbBot);
                }

                _GameManager.IntialiseGame();
                ((MainWindow)Window.GetWindow(this)).MainContent.Content = PageBoard.Instance;
                ((MainWindow)Window.GetWindow(this)).MenuContent.Visibility = Visibility.Hidden;
            }
            catch (Exception exp)
            {
                AlertNotification.Visibility = Visibility.Visible;
                AlertNotification.Content = new AlertDialog(exp, AlertDialog.TypeOfAlert.ERROR);
            }
        }
        #endregion
    }
}
