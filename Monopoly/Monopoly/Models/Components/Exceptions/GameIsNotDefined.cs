using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Exceptions
{
    public class GameIsNotDefined : Exception
    {
        public GameIsNotDefined() : base("Le mode de jeux n'a pas été définie") { }
    }
}
