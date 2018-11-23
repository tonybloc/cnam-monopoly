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
        private static GameManager instance = null;
        public PlayerHandler playerHandler { get; private set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        private GameManager()
        {
            this.playerHandler = PlayerHandler.Instance;
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
        #endregion
    }
}
