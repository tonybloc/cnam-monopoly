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
        public PlayerHandler Instance
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
        /// <param name="player"></param>
        public void AddPlayer(Player player)
        {
            this.ListOfPlayers.Add(player);
        }

        /// <summary>
        /// Enlève un joueur dans la liste
        /// </summary>
        /// <param name="player"></param>
        public void RemovePlayer(Player player)
        {
            this.ListOfPlayers.Remove(player);
        }
        #endregion
    }
}
