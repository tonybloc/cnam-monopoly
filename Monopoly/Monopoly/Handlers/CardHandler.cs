using Monopoly.Models.Components;
using Monopoly.Models.Components.Cards;
using Monopoly.Models.Components.Cells;
using Monopoly.Models.Tools;
using Monopoly.Service.Xml;
using Monopoly.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Handlers
{
    public class CardHandler
    {
        /// <summary>
        /// Instance du gestionnaire
        /// </summary>
        private static CardHandler _instance = null;


        /// <summary>
        /// Player handler
        /// </summary>
        private static PlayerHandler _PlayerHandler { get; set; }

        /// <summary>
        /// Board handler
        /// </summary>
        private static BoardHandler _BoardHandler { get; set; }

        /// <summary>
        /// Pioche : Carte de communiauté
        /// </summary>
        public Deck DeckCommunity { get; private set; }

        /// <summary>
        /// Pioche : Carte de chance
        /// </summary>
        public Deck DeckChance { get; private set; }

        /// <summary>
        /// Current card drawing
        /// </summary>
        public Card CurrentCard { get; private set; }

        /// <summary>
        /// Cration du gestionnaire de carte
        /// </summary>
        private CardHandler()
        {
            DeckCommunity = new Deck();
            DeckCommunity = XmlDataAccess.XMLDeserializeObject<Deck>(Config.filePath_XmlCommunityCard);
            Tools.Shuffle<Card>(DeckCommunity.Cards);

            DeckChance = new Deck();
            DeckChance = XmlDataAccess.XMLDeserializeObject<Deck>(Config.filePath_XmlChanceCard);
            Tools.Shuffle<Card>(DeckChance.Cards);

            _PlayerHandler = PlayerHandler.Instance;
            _BoardHandler = BoardHandler.Instance;
            CurrentCard = null;
        }

        
        // <summary>
        /// Instance du gestionnaire de cartes
        /// </summary>
        public static CardHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CardHandler();
                }
                return _instance;
            }
        }
        /// <summary>
        /// Récupère la prochaine carte du paquet : Communauté
        /// </summary>
        /// <returns></returns>
        public Card GetNextCommunityCard()
        {
            CurrentCard = DeckCommunity.GetNextCard();
            return CurrentCard;
        }

        /// <summary>
        /// Récupère la prochaine carte du paquet : Chance
        /// </summary>
        /// <returns></returns>
        public Card GetNextChanceCard()
        {
            CurrentCard = DeckChance.GetNextCard();
            return CurrentCard;
        }


        public void ExecuteCardAction(Card c)
        {
            if (c is CardUpdateMoney)
            {
                CardUpdateMoney card = (CardUpdateMoney)c;
                if (card.Amount < 0)
                    _PlayerHandler.CardPayeAmount(PlayerHandler.Instance.currentPlayer, Math.Abs(card.Amount));
                else
                    _PlayerHandler.CardGiveMoney(PlayerHandler.Instance.currentPlayer, card.Amount);
            }
            else if (c is CardUpdateMoneyAccordingBuilds)
            {
                CardUpdateMoneyAccordingBuilds card = (CardUpdateMoneyAccordingBuilds)c;
                List<Property> properties = _PlayerHandler.Properties();
                int Amount = 0;
                foreach(Land p in properties)
                {
                    Amount += p.NbHotel * card.CostHotel;
                    Amount += p.NbHouse * card.CostHouse;
                }
                _PlayerHandler.CardPayeAmount(PlayerHandler.Instance.currentPlayer, Math.Abs(Amount));
            }
            else if (c is CardAnniversary)
            {
                CardAnniversary card = (CardAnniversary)c;
                Player current = _PlayerHandler.currentPlayer;
                Predicate<Player> findAllPlayerWithoutDrawer = (Player p) => { return p.Id != current.Id; };
                List<Player> players = _PlayerHandler.ListOfPlayers.FindAll(findAllPlayerWithoutDrawer);
                foreach (Player p in players)
                {
                    _PlayerHandler.PayeTo(p, current, card.Amount);
                }
            }
            else if (c is CardMove)
            {
                CardMove card = (CardMove)c;
                Player current = _PlayerHandler.currentPlayer;
                _PlayerHandler.MoveTo(current, (current.Position + card.NbMove));
            }
            else if (c is CardMoveToCell)
            {
                CardMoveToCell card = (CardMoveToCell)c;
                Player current = _PlayerHandler.currentPlayer;
                _PlayerHandler.MoveTo(current, _BoardHandler.Board.GetCell(card.CellPosition));

            }
            else if (c is CardMoveToJail)
            {
                CardMoveToJail card = (CardMoveToJail)c;
                Player current = _PlayerHandler.currentPlayer;
                _PlayerHandler.MoveTo(current, _BoardHandler.Board.GetCell(card.CellPosition));
            }
            else if (c is CardExitToJail)
            {
                CardExitToJail card = (CardExitToJail)c;
                Player current = _PlayerHandler.currentPlayer;
                switch(card.Action)
                {
                    case CardExitToJail.ActionOption.EXITTOJAIL:
                        _PlayerHandler.ExitToJail(current);
                        break;
                    case CardExitToJail.ActionOption.KEEPCARD:
                        _PlayerHandler.AddCardTo(current, c);
                        break;
                }
            }
            else if (c is CardChoice)
            {
                CardChoice card = (CardChoice)c;
                Player current = _PlayerHandler.currentPlayer;

                switch (card.Action)
                {
                    case CardChoice.ActionOption.DRAWCARD:
                        switch(card.CardTypeToDraw)
                        {
                            case (int)CardType.CHANCE:
                                this.GetNextChanceCard();
                                break;
                            case (int)CardType.COMMUNITY:
                                this.GetNextCommunityCard();
                                break;
                        }
                        break;
                    case CardChoice.ActionOption.PAY:
                        _PlayerHandler.PayeAmount(current, card.Amount);
                        break;
                }
            }
        }
        
    }
}
