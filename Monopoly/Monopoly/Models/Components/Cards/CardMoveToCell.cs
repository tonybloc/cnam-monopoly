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
    [XmlRoot("CardMoveToCell")]
    public class CardMoveToCell : Card
    {
        [XmlElement("CellPosition")]
        public int CellPosition { get; set; }

        [XmlElement("WithStartAmount")]
        public bool withStartAmount { get; set; }

        /// <summary>
        /// Create new instance of class
        /// </summary>
        public CardMoveToCell() { }

    }
}
