using Monopoly.Models.Bank;
using Monopoly.Models.Components;
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

        /// <summary>
        /// First dice
        /// </summary>
        public Dice FirstDice;

        /// <summary>
        /// Second dice
        /// </summary>
        public Dice SecondeDice;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        private GameManager()
        {            
            this.boardHandler = BoardHandler.Instance;
            this.playerHandler = PlayerHandler.Instance;

            FirstDice = new Dice();
            SecondeDice = new Dice();
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

        #region Methods
        public void StartGame()
        {
            CreatePlayer("Jean", "#FF0000");
            CreatePlayer("Marc", "#00FF00");
            CreatePlayer("Camille", "#FFFF00");
            CreatePlayer("Paul", "#0000FF");
            CreatePlayer("Sophie", "#00FFFF");
            CreatePlayer("Lara", "#FF00FF");
            playerHandler.Shuffle(playerHandler.ListOfPlayers);
            playerHandler.InitialisePawnPosition();
            playerHandler.DefineTheNextPlayer();

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

        /// <summary>
        /// Rool dices 
        /// </summary>
        /// <param name="Dice1">First dice</param>
        /// <param name="Dice2">Second dice</param>
        public void RoolDice(Dice Dice1, Dice Dice2)
        {
            Dice1.Rool();
            Dice2.Rool();
        }

        /// <summary>
        /// Calcule the next position of player
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public int NextPosition(Player p, Dice first, Dice second)
        {
            return (p.Position + first.Value + second.Value) % boardHandler.Board.ListCell.Count;
        }

        public void MovePlayerTo(Player p, int position)
        {
            playerHandler.MoveTo(p, position);
        }

        public int GetNumberOfPlayer()
        {
            return playerHandler.GetNumberOfPlayer();
        }
        #endregion
    }
}
