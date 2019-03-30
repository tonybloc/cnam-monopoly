using Monopoly.Handlers;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Cards;
using Monopoly.Models.Components.Cells;
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
    /// Logique d'interaction pour JailInterface.xaml
    /// </summary>
    public partial class JailInterface : UserControl, INotifyPropertyChanged
    {
        public PlayerHandler _PlayerHandler;

        private Player _currentPlayer;
        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                if(_currentPlayer != value)
                {
                    _currentPlayer = value;
                    OnPropertyChanged();
                }
            }
        }

        private CardExitToJail CardExit;

        private bool _canUseCard { get; set; }
        public bool CanUseCard
        {
            get { return _canUseCard; }
            set
            {
                if(_canUseCard != value)
                {
                    _canUseCard = value;
                    OnPropertyChanged();
                }
            }
        }
        
        
        public delegate void UIEventNotifyAlertMessage(string Message, AlertDialog.TypeOfAlert type);
        public static event UIEventNotifyAlertMessage EventNotifyAlertMessage;

        public JailInterface()
        {
            InitializeComponent();
            this.DataContext = this;
            _PlayerHandler = PlayerHandler.Instance;
            CurrentPlayer = _PlayerHandler.currentPlayer;
            CanUseCard = _PlayerHandler.PlayerOwnExitToJailCard(out CardExit);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onClickPayeFees(object sender, EventArgs e)
        {
            _PlayerHandler.PayeAmount(_PlayerHandler.currentPlayer, 50);
            CurrentPlayer.InJail = false;
            CurrentPlayer.NbTurnInJail = 0;
            EventNotifyAlertMessage("Vous etes libéré de prison !", AlertDialog.TypeOfAlert.INFO);
            this.Content = null;

        }

        private void onClickUseCard(object sender, EventArgs e)
        {
            _PlayerHandler.RemoveCardTo(_PlayerHandler.currentPlayer, CardExit);
            CurrentPlayer.InJail = false;
            CurrentPlayer.NbTurnInJail = 0;
            EventNotifyAlertMessage("Vous etes libéré de prison !", AlertDialog.TypeOfAlert.INFO);
            this.Content = null;
        }


        // Event declared : Property Changed
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
