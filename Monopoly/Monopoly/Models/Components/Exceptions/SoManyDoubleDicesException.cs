using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Exceptions
{
    public class SoManyDoubleDicesException : Exception
    {
        public SoManyDoubleDicesException() : base("Vous avez réalisé trop de lancé double. Vous allez en prison.") { }
    }
}
