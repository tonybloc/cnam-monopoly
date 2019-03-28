using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Monopoly.Models.Components.Cards;

namespace Monopoly.Models.Components.Cards
{
    [Serializable]
    [XmlRoot("CardUpdateMoney")]
    public class CardUpdateMoney : Card
    {
        [XmlElement("Amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Create new instance of class
        /// </summary>
        public CardUpdateMoney() { }

        
    }
}
