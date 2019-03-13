using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Exceptions
{
    public class ColorAlreadyAssigned : Exception
    {
        public ColorAlreadyAssigned() : base("La couleur est déjà attribué à un joureur"){}
    }
}
