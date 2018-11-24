using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Handlers
{
    public class GameManager
    {
        #region Variables
        /// <summary>
        /// Game manager instance
        /// </summary>
        private static GameManager instance = null;

        /// <summary>
        /// Player handler
        /// </summary>
        public PlayerHandler playerHandler { get; private set; }
        
        /// <summary>
        /// Board handler
        /// </summary>
        public BoardHandler boardHandler { get; private set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        private GameManager()
        {
            this.playerHandler = PlayerHandler.Instance;
            this.boardHandler = BoardHandler.Instance;
        }

        /// <summary>
        /// Récupère l'instance de la classe
        /// </summary>
        public static GameManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Crée un nouveau joueur
        /// </summary>
        /// <param name="pseudo">pseudo du joueur</param>
        /// <param name="colorValue">couleur du joueur</param>
        public void CreatePlayer(string pseudo, string colorValue )
        {
            playerHandler.CreatePlayer(pseudo, colorValue);
        }
        #endregion
    }
}
