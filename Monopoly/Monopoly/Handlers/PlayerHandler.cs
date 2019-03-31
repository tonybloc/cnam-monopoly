using Monopoly.Models.Bank;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Cards;
using Monopoly.Models.Components.Cells;
using Monopoly.Models.Components.Exceptions;
using Monopoly.Models.Tools;
using Monopoly.Resources.Colors;
using Monopoly.Settings;
using Monopoly.View.Notifications.Dialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Handlers
{
    public class PlayerHandler
    {
        #region Variables
        /// <summary>
        /// Instance of the player handler
        /// </summary>
        private static PlayerHandler instance = null;
        
        /// <summary>
        /// Instance of the bank
        /// </summary>
        private Bank bankInstance = null;
        private BoardHandler _BoardHandler;

        /// <summary>
        /// List  of players
        /// </summary>
        public List<Player> ListOfPlayers { get; private set; }

        public Player currentPlayer { get; private set; }
        
        public delegate void UIEventNotifyAlertMessage(string Message, AlertDialog.TypeOfAlert type);
        public static event UIEventNotifyAlertMessage EventNotifyAlertMessage;
        

        public delegate void UIEventMovePlayerCell(Player p, Cell c, bool startAmount);
        public static event UIEventMovePlayerCell EventMovePlayerToCell;

        public delegate void UIEventRefreshBoard();
        public static event UIEventRefreshBoard EventRefreshBoard;


        
        #endregion



        #region Constructeurs
        /// <summary>
        /// Create an instance of the class
        /// </summary>
        private PlayerHandler()
        {
            this.ListOfPlayers = new List<Player>();
            this.bankInstance = Bank.Instance;
        }

        /// <summary>
        /// Récupère l'unique instance de la classe
        /// </summary>
        public static PlayerHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerHandler();
                }
                return instance;
            }
        }

        public static void Reset()
        {
            instance = null;
        }
        
        #endregion

        #region Methodes (public)


        public void Initialize()
        {
            Tools.Shuffle(ListOfPlayers);
            InitialisePawnPosition();
            DefineTheNextPlayer();
            _BoardHandler = BoardHandler.Instance;
        }

        /// <summary>
        /// Get the liste of colors that was not used by players (for the generation of bots)
        /// </summary>
        /// <returns></returns>
        public List<string> GetAvailablePawnColors()
        {
            
            List<string> colors = new List<string>(PawnColors.Colors);  // Create a copy of the colors list

            foreach (Player p in ListOfPlayers)
                colors.Remove(p.Pawn.ColorValue);
            
            return colors;
        }
     

        /// <summary>
        /// Récupère le nombre de joueur dans la liste
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfPlayer()
        {
            return this.ListOfPlayers.Count;
        }
        /// <summary>
        /// Add a player to the list of players
        /// </summary>
        /// <param name="player">Joueur</param>
        public void AddPlayer(Player player)
        {
            if (ListOfPlayers.Exists(x => x.Pawn.ColorValue == player.Pawn.ColorValue))
            {
                throw new InvalidePlayerColorException();
            }
            else
            {
                this.ListOfPlayers.Add(player);
                bankInstance.CreateBankAccount(player);
            }
            
        }

        /// <summary>
        /// Enlève un joueur dans la liste
        /// </summary>
        /// <param name="player">Joueur</param>
        public void RemovePlayer(Player player)
        {
            this.ListOfPlayers.Remove(player);
        }


        /// <summary>
        /// Définie le prochain joueur à joue
        /// </summary>
        public void DefineTheNextPlayer()
        {
            if (this.ListOfPlayers.Count > 0)
            {
                Player current = GetCurrentPlayer();
                if (current != default(Player))
                {
                    int currentIndex = this.ListOfPlayers.IndexOf(current);
                    Player next = this.ListOfPlayers[(currentIndex + 1) % this.ListOfPlayers.Count];
                    current.Status = Player.StatusOfPlayer.WAITING;
                    next.Status = Player.StatusOfPlayer.PLAYING;
                    
                }
                else
                {
                    this.ListOfPlayers[0].Status = Player.StatusOfPlayer.PLAYING;
                }
            }
            else
            {
                //Throws new Exception(Il n'y a aucun joueur dans la partie)
            }
        }

        /// <summary>
        /// Déplace le joueur à une position cible
        /// </summary>
        /// <param name="p">Joueur</param>
        /// <param name="position">Position</param>
        public void MoveTo(Player p, int position)
        {            
            p.MoveTo((position) % _BoardHandler.Board.ListCell.Count);
        }

        /// <summary>
        /// Déplace le joueur sur une cellule cible
        /// </summary>
        /// <param name="p">Joueur</param>
        /// <param name="c">Cell</param>
        public void MoveTo(Player p, Cell c)
        {
            p.MoveTo(c);
        }

        /// <summary>
        /// Initialise la position des pion (des joueurs)
        /// </summary>
        public void InitialisePawnPosition()
        {
            int j = -1;
            for (int i = 0; i < this.ListOfPlayers.Count; i++)
            {
                if ((i % 3) == 0)
                {
                    j = (j + 1) % 3;
                }

                ListOfPlayers[i].Pawn.X = i % 3;
                ListOfPlayers[i].Pawn.Y = j % 3;
            }
        }
        
        #endregion

        #region Methodes privées
        /// <summary>
        /// Check that the player own that property que le joueur possède bien la propriété
        /// </summary>
        /// <param name="property">Property</param>
        /// <returns>Boolean</returns>
        private bool PlayerOwnProperty(Player player, Property property)
        {
            //Predicate<Property> findProperty = (Property p) => { return p.Id == property.Id; };
            return player.ListOfProperties.Count(p => p.Id == property.Id) != 0;

        }
        /// <summary>
        /// Search the next player
        /// </summary>
        /// <param name="p">Previous player</param>
        /// <returns>The next player</returns>
        private Player findNextPlayer(Player p)
        {
            Predicate<Player> filterPlayer = (Player player) => { return player == p; };
            int index = (ListOfPlayers.FindIndex(filterPlayer)) % ListOfPlayers.Count;
            return ListOfPlayers[index + 1];

        }
        #endregion

        #region Methodes publiques


        /// <summary>
        /// Retrieve the current player
        /// </summary>
        /// <returns>The player who is playing</returns>
        public Player GetCurrentPlayer()
        {            
            Predicate<Player> filtrePlayer = (Player p) => { return p.Status == Player.StatusOfPlayer.PLAYING; };
            currentPlayer =  ListOfPlayers.Find(filtrePlayer);
            return currentPlayer;
            
        }

        /// <summary>
        /// Retrieve the property thanks to its id
        /// </summary>
        /// <param name="idProperty">Id of the property</param>
        /// <returns>The corresponding property</returns>
        public Property GetProperty(int idProperty)
        {
            List<Property> properties = BoardHandler.Instance.Board.ListCell.Where(l => l is Property).Cast<Property>().ToList();

            Predicate<Cell> filtreProperty = (Cell p) => { return p.Id == idProperty; };

            return properties.Find(filtreProperty);

        }


        /// <summary>
        /// Buy a specific property at the bank
        /// </summary>
        /// <param name="property">Propriété</param>
        public void BuyProperty(Property property)
        {
            Player player = GetCurrentPlayer();
            BankAccount account = bankInstance.GetBankAccount(player);
            account.BankTransfer(bankInstance.GetBankAccount(), property.PurchasePrice);
            player.AddPorperty(property);
            property.Status = Property.NOT_AVAILABLE_ON_SALE;
            property.Owner = player;

        }

        /// <summary>
        /// Buy a property owned buy someone else 
        /// </summary>
        /// <param name="player">Player who own the property</param>
        /// <param name="toPlayer"> Player who buy</param>
        /// <param name="property">Property wished</param>
        public bool BuyPropertyTo(Player player, Player toPlayer, Property property)
        {
            if (property.Status == Property.MORTGAGED)
            {
                BankAccount accountPlayer = bankInstance.GetBankAccount(player);
                BankAccount accountToPlayer = bankInstance.GetBankAccount(toPlayer);
                accountPlayer.BankTransfer(accountToPlayer, property.MortgagePrice);
                player.RemoveProperty(property);
                toPlayer.AddPorperty(property);
                property.Status = Property.NOT_AVAILABLE_ON_SALE;

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupe"></param>
        private int CountTheNumberOfLandInLandGroup(LandGroup groupe)
        {
            return BoardHandler.Instance.Board.ListCell.Where(l => l is Land).Cast<Land>().ToList().Where(land => land.LandGroup.IdGroup.Equals(groupe.IdGroup)).Count();
        }

        private int CountTheNumberOfTrainStation()
        {
            return BoardHandler.Instance.Board.ListCell.Where(l => l is TrainStation).Count();
        }

        private int NumberOfTrainStationOwned(Property p )
        {
            return WhoOwnThisProperty(p).ListOfProperties.Where(l => l is TrainStation).Where(l => l.Status != Property.MORTGAGED).Count();
        }

        private int CountTheNumberOfPublicSercices()
        {
            return BoardHandler.Instance.Board.ListCell.Where(l => l is PublicService).Count();
        }

        private int NumberOfPublicServiceOwned(Property p)
        {
            return WhoOwnThisProperty(p).ListOfProperties.Where(l => l is PublicService).Where(l => l.Status != Property.MORTGAGED).Count();
        }

        public bool CheckIfPlayerOwnAllLandInLandGroup(Player player, LandGroup groupe)
        {
            int nbLandPlayer = player.ListOfProperties.Where(l => l.Status != Property.MORTGAGED).Where(l => l is Land).Cast<Land>().ToList().Where(land => land.LandGroup.IdGroup.Equals(groupe.IdGroup)).Count();

            int nbLandBoard = CountTheNumberOfLandInLandGroup(groupe);

            if (nbLandPlayer == nbLandBoard)
                return true;

            return false;
        }

        public List<Land> BuilbingLands()
        {
            Player p = GetCurrentPlayer();
            List<Land> ListOfLands = p.ListOfProperties.Where(l => l.Status != Property.MORTGAGED).Where(l => l is Land).Cast<Land>().OrderBy(l => l.LandGroup.IdGroup).ToList();
            List<Land> BuildingLands = new List<Land>();

            foreach (Land l in ListOfLands)
            {
                if (CheckIfPlayerOwnAllLandInLandGroup(p, l.LandGroup))
                    BuildingLands.Add(l);
            }

            return BuildingLands;
        }

        public ObservableCollection<Property> Properties()
        {
            Player player = GetCurrentPlayer();
            List<Property> properties = player.ListOfProperties.Where(l => l.Status == Property.NOT_AVAILABLE_ON_SALE).ToList();
            List<Property> ListOtherProperties = properties.Where(l => l.GetType() != typeof(Land)).ToList();
            List<Property> ListOfLands = properties.Where(l => l is Land).Cast<Land>().Where(l => l.NbHouse == 0 && l.NbHotel == 0).OrderBy(l => l.LandGroup.IdGroup).Cast<Property>().ToList();

            var res = new ObservableCollection<Property>();
            ListOtherProperties.ForEach(p => res.Add(p));
            ListOfLands.ForEach(p => res.Add(p));
            res.OrderBy(p => p.Id);
            return res;
        }

        public List<Property> MortagedLands()
        {
            Player p = GetCurrentPlayer();

            List<Property> MortgagedLands = p.ListOfProperties.Where(l => l.Status == Property.MORTGAGED).ToList();

            return MortgagedLands;
        }

        public bool CheckIfPlayerOwnThisProperty(Property property)
        {
            return GetCurrentPlayer().ListOfProperties.Any(p => p.Id.Equals(property.Id));
        }

        public Player WhoOwnThisProperty(Property property)
        {
            return ListOfPlayers.Find(player => player.ListOfProperties.Contains(property));
        }

        public void BuildOnLand(Player player, Land land)
        {
            if ((land.NbHouse == Land.NB_MAX_HOUSES) && (land.NbHotel < Land.NB_MAX_HOTEL))
            {
                bankInstance.SellHotel(player, land);

            }

            if ((land.NbHouse < Land.NB_MAX_HOUSES) && (land.NbHotel == 0))
            {
                bankInstance.SellHouse(player, land);
            }
        }

        public void Mortgage(Player player, Property p)
        {
            if (p.Status == Property.NOT_AVAILABLE_ON_SALE)
            {
               p.Status = Property.MORTGAGED;
                bankInstance.Mortgaged(player, p);
                EventNotifyAlertMessage("Vous venez de mettre en hypotèque une propriété", AlertDialog.TypeOfAlert.INFO);
            }
        }

        public void RaiseMortgage(Player player, Property p)
        {
            if (p.Status == Property.MORTGAGED)
            {               
                p.Status = Property.NOT_AVAILABLE_ON_SALE;
                bankInstance.RaiseMortgaged(player, p);
                EventNotifyAlertMessage("Vous venez de récupérer la propriété hypothéqué", AlertDialog.TypeOfAlert.INFO);
            }      
        }

        public void Sell(Player player, Land land)
        {
            if( (land.NbHouse == 0) && (land.NbHotel == 0) )
            {
                EventNotifyAlertMessage("La vente est imposible, vous ne possèder pas de maison ni d'hotel sur la propriété", AlertDialog.TypeOfAlert.ERROR);
            }
            else
            {
                if ((land.NbHouse <= Land.NB_MAX_HOUSES) && (land.NbHotel == 0))
                {
                    bankInstance.BuyHouse(player, land);
                    EventNotifyAlertMessage("Vous venez de vendre une maison", AlertDialog.TypeOfAlert.INFO);
                }

                if ((land.NbHouse == 0) && (land.NbHotel == Land.NB_MAX_HOTEL))
                {
                    bankInstance.BuyHotel(player, land);
                    EventNotifyAlertMessage("Vous venez de vendre un hotel", AlertDialog.TypeOfAlert.INFO);
                }
            }
            

        }

        /// <summary>
        /// 
        /// </summary>
        /// //Le loyer pour un terrain nu (sans bâtiments) est indiqué sur le titre de propriété 
        /// //correspondant. Ce loyer est doublé si le propriétaire possède tous les terrains (non hypothéqués) d’un même groupe de couleur.
        public int PayTheRent(Property p, int dicesValue)
        {
            int rentValue = GetTheRent(p, dicesValue);

            BankAccount accountPlayer = bankInstance.GetBankAccount(GetCurrentPlayer());
            BankAccount accountToPlayer = bankInstance.GetBankAccount(WhoOwnThisProperty(p));
            accountPlayer.BankTransfer(accountToPlayer, rentValue);

            return rentValue;
        }

        public int GetTheRent(Property p, int dicesValue)
        {
            int rentValue = 0;

            if (p is Land)
            {
                Land l = (Land)p;

                if (CheckIfPlayerOwnAllLandInLandGroup(WhoOwnThisProperty(p), l.LandGroup))
                {
                    if (l.NbHouse == 0 && l.NbHotel == 0)
                        rentValue = 2 * l.GetRent(0);
                    else if (l.NbHouse <= Land.NB_MAX_HOUSES && l.NbHotel == 0)
                        rentValue = l.GetRent(l.NbHouse);
                    else if (l.NbHouse == 0 && l.NbHotel != 0)
                        rentValue = l.GetRent(Land.NB_MAX_HOUSES + l.NbHotel);
                }
                else
                    rentValue = l.GetRent(l.NbHouse);
            }
            else if (p is PublicService)
            {
                if (NumberOfPublicServiceOwned(p) == 1)
                    rentValue = 4 * dicesValue;
                else if (NumberOfPublicServiceOwned(p) == CountTheNumberOfPublicSercices())
                    rentValue = 10 * dicesValue;
            }
            else if (p is TrainStation)
            {
                TrainStation t = (TrainStation)p;
                rentValue = t.TrainStationRent * NumberOfTrainStationOwned(p);
            }
            

            return rentValue;
        }



        /// <summary>
        /// When the player is on a tax cell, he pays the tax
        /// </summary>
        /// <param name="p">Player</param>
        /// <param name="t">Tax</param>
        public void PayTheTax(Tax t)
        {
            bankInstance.PayTheTax(GetCurrentPlayer(), t);
        }

        /// <summary>
        /// When the user player pass or stay on the start point, he received a gratification
        /// </summary>
        /// <param name="p">Player</param>
        public void GetGratification(Player p)
        {
            bankInstance.PaymentGratification(p);
        }

        /// <summary>
        /// When the player is on the parking cell, he retrieve all the money 
        /// </summary>
        /// <param name="p">Player</param>
        public void GetParkingMoney()
        {
            bankInstance.GetParkingMoney(GetCurrentPlayer());
        }

        /// <summary>
        /// the player pay amount to player target
        /// </summary>
        /// <param name="p"></param>
        /// <param name="to"></param>
        /// <param name="amount"></param>
        public void PayeTo(Player p, Player target, int amount)
        {
            bankInstance.PlayerPayTo(p,target,amount);
        }

        /// <summary>
        /// The player does pay amount
        /// </summary>
        /// <param name="p">player</param>
        /// <param name="amount">amount</param>
        public void PayeAmount(Player p, int amount)
        {
            Bank.Instance.PlayerPaye(p, amount);
        }

        /// <summary>
        /// Give money to player
        /// </summary>
        /// <param name="p">player</param>
        /// <param name="amount">amount</param>
        public void CardGiveMoney(Player p, int amount)
        {
            Bank.Instance.GiveMoneyTo(p, amount);
        }
        
        /// <summary>
        /// Add card in listeOfCard
        /// </summary>
        /// <param name="p">player</param>
        /// <param name="c">card to add</param>
        public void AddCardTo(Player p, Card c)
        {
            p.AddCard(c);
        }

        /// <summary>
        /// Remove card of player
        /// </summary>
        /// <param name="p">player</param>
        /// <param name="c">card to remove</param>
        public void RemoveCardTo(Player p, Card c)
        {
            p.RemoveCard(c);
        }

        /// <summary>
        /// Player exit to jail
        /// </summary>
        /// <param name="p">player</param>
        public void ExitToJail(Player p)
        {
            currentPlayer.InJail = false;
        }

        /// <summary>
        /// Player go to jail
        /// </summary>
        public void GoToJail()
        {
            currentPlayer.InJail = true;
            currentPlayer.NbTurnInJail = 0;
            EventMovePlayerToCell(currentPlayer, _BoardHandler.Board.GetCell(Config.JAIL_POSITION), false);
            DicesHandler.Instance.PlayerCanBeRaise = false;
        }

        /// <summary>
        /// Check if player own card : exit to jail
        /// </summary>
        /// <param name="card">card</param>
        /// <returns></returns>
        public bool PlayerOwnExitToJailCard(out CardExitToJail card)
        {
            card = (CardExitToJail)currentPlayer.ListOfCards.FirstOrDefault(x => x is CardExitToJail);
            return (card != default(CardExitToJail));

        }

        /// <summary>
        /// Check if player can buy amount
        /// </summary>
        /// <param name="p">player</param>
        /// <param name="Amount">amount to pay</param>
        /// <returns></returns>
        public bool CheckIfPlayerCanBuy(Player p, double Amount)
        {
            return (bankInstance.GetBankAccount(p).Amount >= Amount);            
        }

        /// <summary>
        /// When the player give up
        /// </summary>
        /// <param name="p"></param>
        public void PlayerGiveUp(Player player)
        {
            foreach(Property p in player.ListOfProperties)
            {
                p.Owner = null;
                p.Status = Property.AVAILABLE_ON_SALE;                
            }
            player.ListOfProperties.Clear();
            player.ListOfCards.Clear();

            player.HasLost = true;

            EventRefreshBoard();
        }

        public Player FindWiner()
        {
            return ListOfPlayers.Find(x => (x.IsWinner == true));
        }

        public bool GameIsFinish()
        {
            int nb = ListOfPlayers.Count(x => (x.HasLost == true));
            if (nb == GetNumberOfPlayer()-1)
            {
                Player p = ListOfPlayers.Find(x => (x.HasLost != true));
                p.IsWinner = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
