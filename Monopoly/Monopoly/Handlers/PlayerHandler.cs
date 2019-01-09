using Monopoly.Models.Bank;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Cells;
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

        private static bool alreadyShuffle = false;

        private Bank bankInstance = null;

        /// <summary>
        /// Number of players
        /// </summary>
        private static int numberOfPlayer = 0;

        /// <summary>
        /// List  of players
        /// </summary>
        public List<Player> ListOfPlayers { get; private set; }
        
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
                if(instance == null)
                {
                    instance = new PlayerHandler();
                }
                return instance;
            }
        }
        #endregion

        #region Methodes (public)
        /// <summary>
        /// Récupère le nombre de joeur dans la liste
        /// </summary>
        /// <returns></returns>
        internal int GetNumberOfPlayer()
        {
            return this.ListOfPlayers.Count;
        }
        /// <summary>
        /// Add a player to the list of players
        /// </summary>
        /// <param name="player">Joueur</param>
        public void AddPlayer(Player player)
        {
            this.ListOfPlayers.Add(player);
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
            this.AddPlayer(new Player(ListOfPlayers.Count, pseudo, new Pawn(colorValue)));
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
            if(this.ListOfPlayers.Count > 0)
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
        internal void InitialisePawnPosition()
        {
            int j = -1;
            for(int i=0; i<this.ListOfPlayers.Count; i++)
            {
                if( (i%3) == 0)
                {
                    j = (j + 1) % 3;
                }

                ListOfPlayers[i].Pawn.X = i%3;
                ListOfPlayers[i].Pawn.Y = j%3;
            }
        }
        internal void Shuffle(List<Player> list)
        {
            // Only one shuffle !
            if(!alreadyShuffle)
            {
                Random rand = new Random();
                int nbMotion = list.Count;
                while (nbMotion > 1)
                {
                    nbMotion--;
                    int randomIndex = rand.Next(nbMotion + 1);
                    Player player = list[randomIndex];
                    list[randomIndex] = list[nbMotion];
                    list[nbMotion] = player;
                }
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
        /// Buy a specific property at the bank
        /// </summary>
        /// <param name="property">Propriété</param>
        public bool BuyProperty(Player player, Property property)
        {
            if (property.status == Property.AVAILABLE_ON_SALE)
            {
                BankAccount account = bankInstance.GetBankAccount(player);
                account.BankTransfer(Bank.Instance.GetBankAccount(), property.PurchasePrice);
                player.AddPorperty(property);
                property.status = Property.NOT_AVAILABLE_ON_SALE;

                return true;
            }
            //Property not available
            return false;
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
            int compteur = 0;

            Player p = GetCurrentPlayer();
            compteur = p.ListOfProperties.Count();
                //Where(i => i.Id == groupe.IdGroup).LongCount;
            /*
            if(property.GetType() == typeof(Land))
            {
                ((Land)property).LandGroup.IdGroup
            }
            */
            return compteur;
        }
        public bool CheckIfPlayerOwnAllLandInLandGroup(Player player, Property property)
        {
            /*

            Func<Property, bool> filter = CountTheNumberOfLandInLandGroup();
            bank.find
            return false;
            */
            return false;
        }

        public void BuildOnLand(Player player, Land land)
        {
            //BankAccount account = Bank.Instance.GetBankAccount(player);
            //int LandRantal = ((land.NbHouse > 0) &&()) ? land.GetRental();
            //account.BankTransfer(Bank.Instance.GetBankAccount(), land.GetRental();

        }
        #endregion
    }
}
