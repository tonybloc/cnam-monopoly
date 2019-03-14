using Monopoly.Models.Components;
using Monopoly.Models.Components.Exceptions;
using Monopoly.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Handlers
{
    public class DicesHandler
    {

        private static DicesHandler _instance = null;
        
        public Dice FirstDice { get; private set; }
        public Dice SecondDice { get; private set; }
        private Player CurrentLauncher { get; set; }
        private int NumberOfRool { get; set; }
        public bool PlayerCanBeRaise { get; set; } 

        private DicesHandler()
        {
            FirstDice = new Dice();
            SecondDice = new Dice();
            this.CurrentLauncher = new Player();
            this.NumberOfRool = 0;
            this.PlayerCanBeRaise = true;
        }

        public static DicesHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DicesHandler();
                }
                return _instance;
            }            
        }

        /// <summary>
        /// Rool dices 
        /// </summary>
        /// <param name="launcher">the player who rool dices</param>
        public void RoolDices(Player launcher)
        {
            // Define the launcher
            if ((this.CurrentLauncher.Id != launcher.Id) )
            {
                this.CurrentLauncher = launcher;
                this.NumberOfRool = 0;
                this.PlayerCanBeRaise = true;
            }

            if (!PlayerCanBeRaise)
                throw new YouCanNotRoolDicesException();
            
            this.NumberOfRool++;
            FirstDice.Rool();
            SecondDice.Rool();
            this.PlayerCanBeRaise = IsDouble();

            if (NumberOfRool == Config.NB_MAX_LAUNCH_DICES)
            {
                throw new SoManyDoubleDicesException();
            }
           
        }

        
        /// <summary>
        /// Check if the dices launch is double 
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsDouble()
        {
            return (FirstDice.Value == SecondDice.Value);
        }

        /// <summary>
        /// Return the values of dices
        /// </summary>
        /// <returns></returns>
        public int GetValue()
        {
            return FirstDice.Value + SecondDice.Value;
        }
    }
}
