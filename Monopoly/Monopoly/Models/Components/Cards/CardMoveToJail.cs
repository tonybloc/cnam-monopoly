using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cards
{
    [Serializable]
    [XmlRoot("CardMoveToJail")]
    public class CardMoveToJail : Card
    {
        [XmlElement("CellPosition")]
        public int CellPosition { get; set; }

        /// <summary>
        /// Create new instance of class
        /// </summary>
        public CardMoveToJail(): base() { }
                
    }
}
