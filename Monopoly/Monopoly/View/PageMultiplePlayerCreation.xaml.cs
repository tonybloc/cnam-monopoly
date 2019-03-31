using Monopoly.Handlers;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Exceptions;
using Monopoly.View.Notifications.Dialog;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monopoly.View
{
    /// <summary>
    /// Logique d'interaction pour PageMultiplePlayerCreation.xaml
    /// </summary>
    public partial class PageMultiplePlayerCreation : Page, INotifyPropertyChanged
    {
        #region Variables
        private static ColorHandler _ColorHandler;
        private static GameManager _GameManager;
        private static PlayerHandler _PlayerHandler;

        private const string placeholder = "Enter your pseudo...";
        private string defaultColorValue = "#FFFFFF";
       
        public ObservableCollection<Player> ListOfPlayers { get; set; }

        private int _indexSelected;
        public int IndexSelected
        {
            get
            {
                return _indexSelected;
            }
            set
            {
                if(_indexSelected != value)
                {
                    _indexSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private Player _playerSelected;
        public Player PlayerSelected
        {
            get
            {
                return _playerSelected;
            }
            set
            {
                if(_playerSelected != value)
                {
                    _playerSelected = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private string _playerName;
        public string PlayerName
        {
            get
            {
                return _playerName;
            }
            set
            {
                if(_playerName != value)
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
                if(_playerColor != value)
                {
                    _playerColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        /// <summary>
        /// Create new instance of class
        /// </summary>
        public PageMultiplePlayerCreation()
        {
            InitializeComponent();

            DataContext = this;

            _GameManager = GameManager.Instance;
            _ColorHandler = ColorHandler.Instance;
            _PlayerHandler = PlayerHandler.Instance;

            ListOfPlayers = new ObservableCollection<Player>();

            PlayerName = placeholder;
            PlayerColor = defaultColorValue;

            IndexSelected = -1;
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


        private void onClickAddPlayer(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PlayerName == placeholder)
                {
                    UINotifyAlertMessage("Le nom du joueur est invalide !", AlertDialog.TypeOfAlert.WARNING);
                }
                else
                {
                    if (IndexSelected != -1)
                    {
                        if ( ListOfPlayers.Count( x => (x.Pawn.ColorValue == PlayerColor) && (x.Id != PlayerSelected.Id) ) != 0 )
                        {
                            UINotifyAlertMessage("La couleur est déjà attribuée à un joueur", AlertDialog.TypeOfAlert.WARNING);
                        }
                        else
                        {
                            Player p = ListOfPlayers.Single(x => x.Id == PlayerSelected.Id);
                            p.Name = PlayerName;
                            p.Pawn.ColorValue = PlayerColor;
                            OnPropertyChanged("ListOfPlayers");
                            PlayerName = placeholder;
                            PlayerColor = defaultColorValue;
                            _ColorHandler.SetColorIndex(0);
                        }
                    }
                    else
                    {
                        if ((ListOfPlayers.Count(x => (x.Pawn.ColorValue == PlayerColor)) != 0))
                        {
                            UINotifyAlertMessage("La couleur est déjà attribuée à un joueur", AlertDialog.TypeOfAlert.WARNING);
                        }
                        else
                        {
                            this.ListOfPlayers.Add(new Player(PlayerName, new Pawn(PlayerColor), Player.TypeOfPlayer.USER));
                            PlayerName = placeholder;
                            PlayerColor = defaultColorValue;
                            _ColorHandler.SetColorIndex(0);
                        }
                        
                    }
                }
            }
            catch (Exception exp)
            {
                UINotifyAlertMessage(exp.Message, AlertDialog.TypeOfAlert.ERROR);
            }
            
        }
        private void onClickRemovePlayer(object sender, RoutedEventArgs e)
        {
            IndexSelected = -1;
            PlayerName = placeholder;
            PlayerColor = defaultColorValue;
            _ColorHandler.SetColorIndex(0);
        }

        private void onClickBack(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).MainContent.Content = null;
            ((MainWindow)Window.GetWindow(this)).MenuContent.Visibility = Visibility.Visible;
        }

        private void onClickPlay(object sender, RoutedEventArgs e)
        {
            foreach(Player p in ListOfPlayers)
            {
                _PlayerHandler.AddPlayer(p);
            }

            _GameManager.IntialiseGame();
            ((MainWindow)Window.GetWindow(this)).MainContent.Content = PageBoard.Instance;
            ((MainWindow)Window.GetWindow(this)).MenuContent.Visibility = Visibility.Hidden;
        }
    
        private void onSelecionChangeListeViewPlayers(object sender, SelectionChangedEventArgs e)
        {
            if(IndexSelected != -1)
            {
                PlayerColor = PlayerSelected.Pawn.ColorValue;
                PlayerName = PlayerSelected.Name;
            }
            else
            {
                PlayerColor = defaultColorValue;
                PlayerName = placeholder;
            }
            
        }

        
        /// <summary>
        /// Methode to display message alert in view
        /// </summary>
        /// <param name="Message">Message to show</param>
        /// <param name="type">Type of alert</param>
        public void UINotifyAlertMessage(string Message, AlertDialog.TypeOfAlert type)
        {
            AlertNotifcation.Content = new AlertDialog(Message, type);
            AlertNotifcation.Visibility = Visibility.Visible;
        }
        #endregion

    }
}
