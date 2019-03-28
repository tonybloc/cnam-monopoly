using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Monopoly.Handlers;
using Monopoly.Models.Components.Cards;

namespace Monopoly.Models.Components.Cards
{
    [Serializable]
    [XmlRoot("CardUpdateMoneyAccordingBuilds")]
    public class CardUpdateMoneyAccordingBuilds : Card
    {
        [XmlElement("CostHotel")]
        public int CostHotel { get; set; }

        [XmlElement("CostHouse")]
        public int CostHouse { get; set; }
        
        /// <summary>
        /// Create new instance of class
        /// </summary>
        public CardUpdateMoneyAccordingBuilds() { }
        

    }
}
