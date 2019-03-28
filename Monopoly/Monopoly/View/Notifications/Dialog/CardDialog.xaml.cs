using Monopoly.Handlers;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Cards;
using Monopoly.Models.Components.Cells;
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
    /// Logique d'interaction pour CardDialog.xaml
    /// </summary>
    public partial class CardDialog : UserControl, INotifyPropertyChanged
    {
        #region Variables

        

        // Notify Property Changed Event
        public event PropertyChangedEventHandler PropertyChanged;

        // delegate event
        public delegate void UIEventMovePlayer(Player p, int move, bool startAmount);
        public static event UIEventMovePlayer EventMovePlayer;

        public delegate void UIEventMovePlayerCell(Player p, Cell c, bool startAmount);
        public static event UIEventMovePlayerCell EventMovePlayerToCell;

        // Handler
        private CardHandler _CardHandler;

        // Card to display
        private Card _currentCard;
        public Card CurrentCard
        {
            get
            {
                return _currentCard;
            }
            set
            {
                if(_currentCard != value)
                {
                    _currentCard = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion
        /// <summary>
        /// Create new instance of class
        /// </summary>
        /// <param name="c">Card to display</param>
        public CardDialog(Card c)
        {
            InitializeComponent();
            this.DataContext = this;

            _CardHandler = CardHandler.Instance;
            CurrentCard = c;
        }

        
        // Event declared : Property Changed
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Click Events

        // Event for CardChoice
        private void CardChoice_OnClickPay(object sender, EventArgs e)
        {
            CardChoice card = (CardChoice)_CardHandler.CurrentCard;
            card.Action = CardChoice.ActionOption.PAY;
            _CardHandler.ExecuteCardAction(card);
            this.Content = null;

        }
        private void CardChoice_OnClickDrawNewCard(object sender, EventArgs e)
        {
            CardChoice card = (CardChoice)_CardHandler.CurrentCard;
            card.Action = CardChoice.ActionOption.DRAWCARD;
            _CardHandler.ExecuteCardAction(card);
            this.Content = new CardDialog(_CardHandler.CurrentCard);

        }

        // Event for CardAnniversary
        private void CardAnniversary_OnClickValidate(object sender, EventArgs e)
        {
            CardAnniversary card = (CardAnniversary)_CardHandler.CurrentCard;
            _CardHandler.ExecuteCardAction(card);
            this.Content = null;

        }

        // Event for CardExitToJail
        private void CardExitToJail_OnClickUseCard(object sender, EventArgs e)
        {
            CardExitToJail card = (CardExitToJail)_CardHandler.CurrentCard;
            card.Action = CardExitToJail.ActionOption.EXITTOJAIL;
            _CardHandler.ExecuteCardAction(card);
            this.Content = null;

        }        
        private void CardExitToJail_OnClickGetCard(object sender, EventArgs e)
        {
            CardExitToJail card = (CardExitToJail)_CardHandler.CurrentCard;
            card.Action = CardExitToJail.ActionOption.KEEPCARD;
            _CardHandler.ExecuteCardAction(card);
            this.Content = null;
        }

        // Event for CardMove
        private void CardMove_OnClickValidate(object sender, EventArgs e)
        {
            CardMove card = (CardMove)_CardHandler.CurrentCard;
            EventMovePlayer(PlayerHandler.Instance.currentPlayer, card.NbMove, true);
            _CardHandler.ExecuteCardAction(card);
            
            this.Content = null;
        }

        // Event for CardMoveToCell
        private void CardMoveToCell_OnClickValidate(object sender, EventArgs e)
        {
            CardMoveToCell card = (CardMoveToCell)_CardHandler.CurrentCard;
            
            EventMovePlayerToCell(PlayerHandler.Instance.currentPlayer, BoardHandler.Instance.Board.GetCell(card.CellPosition), card.withStartAmount);
            _CardHandler.ExecuteCardAction(card);
            this.Content = null;
        }

        // Event for CardMoveToJail
        private void CardMoveToJail_OnClickValidate(object sender, EventArgs e)
        {
            CardMoveToJail card = (CardMoveToJail)_CardHandler.CurrentCard;
            EventMovePlayerToCell(PlayerHandler.Instance.currentPlayer, BoardHandler.Instance.Board.GetCell(card.CellPosition), false);
            _CardHandler.ExecuteCardAction(card);

            this.Content = null;
        }

        // Event for CardUpdateMoney
        private void CardUpdateMoney_OnClickValidate(object sender, EventArgs e)
        {
            CardUpdateMoney card = (CardUpdateMoney)_CardHandler.CurrentCard;
            _CardHandler.ExecuteCardAction(card);
            this.Content = null;

        }

        //Event for CardUpdateMoneyAccordingBuilds
        private void CardUpdateMoneyAccordingBuilds_OnClickValidate(object sender, EventArgs e)
        {
            CardUpdateMoneyAccordingBuilds card = (CardUpdateMoneyAccordingBuilds)_CardHandler.CurrentCard;
            _CardHandler.ExecuteCardAction(card);
            this.Content = null;

        }
        
        #endregion


    }
}
