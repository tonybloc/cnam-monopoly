using Monopoly.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cards
{
    [Serializable]
    [XmlRoot("CardAnniversary")]
    public class CardAnniversary : Card
    {
        /// <summary>
        /// Amount to pay
        /// </summary>
        [XmlElement("Amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Create new instance of CardAnniversary
        /// </summary>
        public CardAnniversary() { }
        
    }
}
