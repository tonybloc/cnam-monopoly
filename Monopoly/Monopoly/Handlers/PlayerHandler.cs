using Monopoly.Models.Bank;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Cells;
using Monopoly.Models.Components.Exceptions;
using Monopoly.Resources.Colors;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Number of players
        /// </summary>
        private static int numberOfPlayer = 0;

        /// <summary>
        /// List  of players
        /// </summary>
        public List<Player> ListOfPlayers { get; private set; }

        public Player currentPlayer { get; private set; }


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

       
        #endregion

        #region Methodes (public)


        public void Initialize()
        {
            InitialisePawnPosition();
            DefineTheNextPlayer();
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
                throw new ColorAlreadyAssigned();
            }
            else
            {
                this.ListOfPlayers.Add(player);
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
        /// Add a new player to the list of participants
        /// </summary>
        /// <param name="pseudo">Pseudo of the player</param>
        /// <param name="colorValue">Color of the pawn</param>
        public void CreatePlayer(string pseudo, string colorValue)
        {   
            numberOfPlayer++;
            Player p = new Player(numberOfPlayer, pseudo, new Pawn(colorValue));
            this.AddPlayer(p);
            bankInstance.CreateBankAccount(p);
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
                    current.Status = Player.WAITING;
                    next.Status = Player.PLAYING;
                    
                }
                else
                {
                    this.ListOfPlayers[0].Status = Player.PLAYING;
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
            p.MoveTo(position);
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
            Predicate<Property> findProperty = (Property p) => { return p.Id == property.Id; };
            return player.ListOfProperties.Find(findProperty) != null;

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
            
            Predicate<Player> filtrePlayer = (Player p) => { return p.Status == Player.PLAYING; };
            return ListOfPlayers.Find(filtrePlayer);
            
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
            property.status = Property.NOT_AVAILABLE_ON_SALE;
            property.OwnerName = player.Name;

        }

        /// <summary>
        /// Buy a property owned buy someone else 
        /// </summary>
        /// <param name="player">Player who own the property</param>
        /// <param name="toPlayer"> Player who buy</param>
        /// <param name="property">Property wished</param>
        public bool BuyPropertyTo(Player player, Player toPlayer, Property property)
        {
            if (property.status == Property.MORTGAGED)
            {
                BankAccount accountPlayer = bankInstance.GetBankAccount(player);
                BankAccount accountToPlayer = bankInstance.GetBankAccount(toPlayer);
                accountPlayer.BankTransfer(accountToPlayer, property.MortgagePrice);
                player.RemoveProperty(property);
                toPlayer.AddPorperty(property);
                property.status = Property.NOT_AVAILABLE_ON_SALE;

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

        private int NumberOfTrainStationOwned()
        {
            return GetCurrentPlayer().ListOfProperties.Where(l => l is TrainStation).Count();
        }

        private int CountTheNumberOfPublicSercices()
        {
            return BoardHandler.Instance.Board.ListCell.Where(l => l is PublicService).Count();
        }

        private int NumberOfPublicServiceOwned()
        {
            return GetCurrentPlayer().ListOfProperties.Where(l => l is PublicService).Count();
        }

        public bool CheckIfPlayerOwnAllLandInLandGroup(Player player, LandGroup groupe)
        {
            int nbLandPlayer = player.ListOfProperties.Where(l => l is Land).Cast<Land>().ToList().Where(land => land.LandGroup.IdGroup.Equals(groupe.IdGroup)).Count();

            int nbLandBoard = CountTheNumberOfLandInLandGroup(groupe);

            if (nbLandPlayer == nbLandBoard)
                return true;

            return false;
        }

        public List<Land> BuilbingLands()
        {
            Player p = GetCurrentPlayer();
            List<Land> ListOfLands = p.ListOfProperties.Where(l => l is Land).Cast<Land>().OrderBy(l => l.LandGroup.IdGroup).ToList();
            List<Land> BuildingLands = new List<Land>();

            foreach (Land l in ListOfLands)
            {
                if (CheckIfPlayerOwnAllLandInLandGroup(p, l.LandGroup))
                    BuildingLands.Add(l);
            }

            return BuildingLands;
        }

        public List<Property> Properties()
        {
            Player player = GetCurrentPlayer();
            return player.ListOfProperties.Where(l => l.status == Land.NOT_AVAILABLE_ON_SALE).OrderBy(p => p.Id).ToList();
        }

        public List<Property> MortagedLands()
        {
            Player p = GetCurrentPlayer();
            List<Property> MortgagedLands = p.ListOfProperties.Where(l => l.status == Land.MORTGAGED).ToList();

            return MortgagedLands;
        }

        public bool CheckIfPlayerOwnThisProperty(Property property)
        {
            return GetCurrentPlayer().ListOfProperties.Any(p => p.Id.Equals(property.Id));
        }

        private Player WhoOwnThisProperty(Property property)
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
            p.status = Property.MORTGAGED;
            bankInstance.Mortgaged(player, p);
        }

        public void RaiseMortgage(Player player, Property p)
        {
            p.status = Property.NOT_AVAILABLE_ON_SALE;
            bankInstance.RaiseMortgaged(player, p);
        }

        public void Sell(Player player, Land land)
        {
            if ((land.NbHouse <= Land.NB_MAX_HOUSES) && (land.NbHotel == 0))
            {
                bankInstance.BuyHouse(player, land);
            }

            if ((land.NbHouse == 0) && (land.NbHotel == Land.NB_MAX_HOTEL))
            {
                bankInstance.BuyHotel(player, land);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// //Le loyer pour un terrain nu (sans bâtiments) est indiqué sur le titre de propriété 
        /// //correspondant. Ce loyer est doublé si le propriétaire possède tous les terrains (non hypothéqués) d’un même groupe de couleur.
        public void PayTheRent(Property p, int dicesValue)
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
                if (NumberOfPublicServiceOwned() == 1)
                    rentValue = 4 * dicesValue;
                else if (NumberOfPublicServiceOwned() == CountTheNumberOfPublicSercices())
                    rentValue = 10 * dicesValue;
            }
            else if (p is TrainStation)
            {
                TrainStation t = (TrainStation)p;
                rentValue = t.TrainStationRent * NumberOfTrainStationOwned();
            }

            BankAccount accountPlayer = bankInstance.GetBankAccount(GetCurrentPlayer());
            BankAccount accountToPlayer = bankInstance.GetBankAccount(WhoOwnThisProperty(p));
            accountPlayer.BankTransfer(accountToPlayer, rentValue);
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
        #endregion
    }
}
