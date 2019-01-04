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
        private static PlayerHandler instance = null;
        private static bool alreadyShuffle = false;

        public List<Player> ListOfPlayers { get; private set; }
        
        #endregion

        #region Constructeurs
        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        private PlayerHandler()
        {
            this.ListOfPlayers = new List<Player>();
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
        /// Ajoute un joueur dans la liste
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
        /// Ajoute un nouveau joueur dans la liste des participants
        /// </summary>
        /// <param name="pseudo">pseudo du joueur</param>
        /// <param name="colorValue">couleur du pion</param>
        public void CreatePlayer(string pseudo, string colorValue)
        {
            this.AddPlayer(new Player(ListOfPlayers.Count, pseudo, new Pawn(colorValue)));
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
        /// Récupère le joueur courant
        /// </summary>
        /// <returns></returns>
        public Player GetCurrentPlayer()
        {
            Predicate<Player> filtrePlayer = (Player p) => { return p.Status == Player.PLAYING; };
            return ListOfPlayers.Find(filtrePlayer);
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
    }
}
