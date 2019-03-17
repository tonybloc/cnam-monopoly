using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Exceptions
{
    public class InvalidePlayerNameException : Exception
    {
        public InvalidePlayerNameException() : base("Le nom du joueur est invalide !") {}
    }
}
