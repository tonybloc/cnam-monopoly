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

        private List<Player> ListOfPlayers { get; set; }

        #endregion

        #region Constructeurs
        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        public PlayerHandler()
        {
            this.ListOfPlayers = new List<Player>();
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
