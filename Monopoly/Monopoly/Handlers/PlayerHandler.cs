using Monopoly.Models.Components;
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
        private static int numberOfPlayer = 0;
        public List<Player> ListOfPlayers { get; private set; }
        
        #endregion

        #region Constructeurs
        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        private PlayerHandler()
        {
            numberOfPlayer = 0;
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
            numberOfPlayer++;
            this.AddPlayer(new Player(numberOfPlayer, pseudo, new Pawn(colorValue)));
        }
        #endregion
    }
}
