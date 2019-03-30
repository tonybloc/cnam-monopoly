using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Exceptions
{
    public class InvalidePlayerColorException : Exception
    {
        public InvalidePlayerColorException() : base("La couleur est déjà attribuée à un joueur"){}
    }
}
