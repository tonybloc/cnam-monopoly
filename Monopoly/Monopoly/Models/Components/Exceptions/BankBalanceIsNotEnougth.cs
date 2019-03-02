using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Exceptions
{
    public class BankBalanceIsNotEnougth : Exception
    {
        public BankBalanceIsNotEnougth() : base("Votre solde bancaire n\'est pas suffisant")
        {

        }
    }
}
