using Monopoly.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Exceptions
{
    public class InvalideNumberOfPlayerInGameException : Exception
    {
        public InvalideNumberOfPlayerInGameException() : base("Le nombre de joueurs est invalide. Nombre minimum : " + Config.NB_MIN_PLAYER_IN_GAME + "; Nombre maximum : " + Config.NB_MAX_PLAYER_IN_GAME) {}
    }
}
