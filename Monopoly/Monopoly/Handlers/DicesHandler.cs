using Monopoly.Models.Components;
using Monopoly.Models.Components.Cells;
using Monopoly.Models.Components.Exceptions;
using Monopoly.Settings;
using Monopoly.View.Notifications.Dialog;
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

        public delegate void UIEventNotifyMessage(string Message);
        public static event UIEventNotifyMessage EventNotifyMessage;

        public delegate void UIEventNotifyAlertMessage(string Message, AlertDialog.TypeOfAlert type);
        public static event UIEventNotifyAlertMessage EventNotifyAlertMessage;

        public delegate void UIEventMovePlayer(Player p, int move, bool startAmount);
        public static event UIEventMovePlayer EventMovePlayer;

        public delegate void UIEventMovePlayerCell(Player p, Cell c, bool startAmount);
        public static event UIEventMovePlayerCell EventMovePlayerToCell;

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

        public static void Reset()
        {
            _instance = null;
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
            {
                EventNotifyMessage("Vous ne pouvez pas relancer les dés !");
                
            }else if (NumberOfRool >= Config.NB_MAX_LAUNCH_DICES)
            {
                this.PlayerCanBeRaise = false;
                launcher.InJail = true;
                EventNotifyMessage("Vous avez réalisé trop de lancé double. Vous allez en prison.");
                EventMovePlayerToCell(CurrentLauncher, BoardHandler.Instance.Board.GetCell(Config.JAIL_POSITION), false);
            }
            else
            {
                this.NumberOfRool++;
                FirstDice.Rool();
                SecondDice.Rool();
                if(IsDouble())
                {
                    if( (this.CurrentLauncher.InJail))
                    {
                        CurrentLauncher.NbTurnInJail = 0;
                        this.CurrentLauncher.InJail = false;
                        this.PlayerCanBeRaise = true;
                        EventNotifyAlertMessage("Vous êtes libéré de prison !", AlertDialog.TypeOfAlert.INFO);
                        EventMovePlayer(CurrentLauncher, GetValue(), true);
                    }
                    else
                    {
                        this.PlayerCanBeRaise = true;
                        EventMovePlayer(CurrentLauncher, GetValue(), true);
                    }
                    
                }
                else
                {
                    if(this.CurrentLauncher.InJail)
                    {
                        CurrentLauncher.NbTurnInJail += 1;
                        this.PlayerCanBeRaise = false;
                        EventNotifyAlertMessage("Vous êtes toujours en prison !", AlertDialog.TypeOfAlert.INFO);
                    }
                    else
                    {
                        this.PlayerCanBeRaise = false;
                        EventMovePlayer(CurrentLauncher, GetValue(), true);
                    }
                    
                }
            }
            
        }

        
        /// <summary>
        /// Check if the dices launch is double 
        /// </summary>
        /// <returns>true/false</returns>
        private bool IsDouble()
        {
            return (FirstDice.Value == SecondDice.Value);
        }

        
        /// <summary>
        /// Return the values of dices
        /// </summary>
        /// <returns></returns>
        private int GetValue()
        {
            return FirstDice.Value + SecondDice.Value;
        }
        
    }
}
