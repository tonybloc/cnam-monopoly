using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Exceptions
{
    public class UserAsMissingToLaunchDicesException : Exception
    {
        public UserAsMissingToLaunchDicesException() : base("Vous avez oublié de lancer les dés") { }
    }
}
