using Monopoly.Models.Bank;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Exceptions;
using Monopoly.Models.Tools;
using Monopoly.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private static GameManager _instance = null;        

        /// <summary>
        /// Player handler
        /// </summary>
        public PlayerHandler PlayerHandler { get; private set; }
        
        /// <summary>
        /// Board handler
        /// </summary>
        public BoardHandler BoardHandler { get; private set; }

        /// <summary>
        /// Dice handler
        /// </summary>
        public DicesHandler DicesHandler { get; private set; }
        
        
        /// <summary>
        /// Time of game
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Cout the number of turn
        /// </summary>
        public int NumberOfTurn{ get; set; }

        

        /// <summary>
        /// Status of game
        /// </summary>
        public GameStatus Status { get; private set; }

        /// <summary>
        /// Type of game
        /// </summary>
        private GameType Type { get; set; }


        public enum GameStatus : int { START = 0, STOP = 1, END = 2 };
        public enum GameType : int { SINGLEPLAYER = 0, LOCAL_MUTLIPLAYER = 1, NETWORK_MUTLIPLAYER = 2 }

        #endregion

        #region Constructeur
        /// <summary>
        /// Create new instance of GameManager
        /// </summary>
        private GameManager()
        {            
            this.BoardHandler = BoardHandler.Instance;
            this.PlayerHandler = PlayerHandler.Instance;
            this.DicesHandler = DicesHandler.Instance;
        }

        /// <summary>
        /// Get the instance of the class
        /// </summary>
        public static GameManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
        
        /// <summary>
        /// Set the type of the game
        /// </summary>
        /// <param name="type">type of game</param>
        public void SetType(GameType type)
        {
            this.Type = type;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Initialise the game
        /// </summary>
        public void IntialiseGame()
        {

            int numberOfPlayerInGame = this.PlayerHandler.GetNumberOfPlayer();
            
            if( (numberOfPlayerInGame <= Config.NB_MAX_PLAYER_IN_GAME))
            {
                GeneratedBot(Config.NB_MAX_PLAYER_IN_GAME - numberOfPlayerInGame);
            }
            else
            {
                throw new InvalideNumberOfPlayerInGameException();
            }
           
            this.PlayerHandler.Initialize();
            
        }

        

        /// <summary>
        /// Start the game
        /// </summary>
        public void StartGame()
        {
            this.Status = GameStatus.START;
        }

        /// <summary>
        /// Stop the game
        /// </summary>
        public void StopGame()
        {
            this.Status = GameStatus.STOP;
        }

        /// <summary>
        /// End the game
        /// </summary>
        public void EndGame()
        {
            this.Status = GameStatus.END;
        }

       

        /// <summary>
        /// Crée un nouveau joueur
        /// </summary>
        /// <param name="pseudo">pseudo du joueur</param>
        /// <param name="colorValue">couleur du joueur</param>
        public void CreatePlayer(string pseudo, string colorValue )
        {
            PlayerHandler.CreatePlayer(pseudo, colorValue);
        }

        #endregion

        public void NextTurn()
        {
            this.PlayerHandler.DefineTheNextPlayer();
            int index = this.PlayerHandler.ListOfPlayers.IndexOf(this.PlayerHandler.GetCurrentPlayer());
            if (index == 0 )
            {
                this.NumberOfTurn++;
            }

        }


        /// <summary>
        /// Get the next position of player
        /// </summary>
        /// <param name="numberOfMove">Number of movement</param>
        public int NextPosition(Player p, int numberOfMove)
        {
            return (p.Position + numberOfMove) % BoardHandler.Board.ListCell.Count;
        }


        /// <summary>
        /// Generate some bot for the game
        /// </summary>
        /// <param name="numberOfBot"></param>
        private void GeneratedBot(int numberOfBot)
        {
            List<string> BotColors = PlayerHandler.GetAvailablePawnColors();
            List<string> BotName = Config.BotNames;
            Tools.Shuffle(BotName);
            Tools.Shuffle(BotColors);


            for (int i = 0; i < numberOfBot; i++)
            {
                Player p = new Player(100 + i, BotName[i], new Pawn( BotColors[i] ) );
                p.SetPlayerType( Player.TypeOfPlayer.BOT );
                PlayerHandler.AddPlayer(p);
            }
        }
    }
}
