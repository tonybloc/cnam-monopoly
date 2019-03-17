using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Exceptions
{
    public class YouCanNotRoolDicesException : Exception
    {
        public YouCanNotRoolDicesException() : base("Vous ne pouvez pas relancer les dés !"){}
    }
}
