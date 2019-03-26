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
    [XmlRoot("CardMove")]
    public class CardMove : Card
    {
        [XmlElement("NbMove")]
        public int NbMove { get; set; }

        /// <summary>
        /// Create new instance of class
        /// </summary>
        public CardMove() { }
       
    }
}
