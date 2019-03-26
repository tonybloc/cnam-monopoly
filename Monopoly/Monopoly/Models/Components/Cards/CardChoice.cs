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
    [XmlRoot("CardChoice")]
    public class CardChoice : Card
    {       
        [XmlElement("CardTypeToDraw")]
        public int CardTypeToDraw { get; set; }
        
        [XmlElement("Amount")]
        public int Amount { get; set; }

        [XmlIgnore]
        public ActionOption Action { get; set; }
        public enum ActionOption : int { NODEFINED = 0, DRAWCARD = 1, PAY = 2 };
        
        /// <summary>
        /// Create new instance of class
        /// </summary>
        public CardChoice()
        {
            Action = ActionOption.NODEFINED;
        }
        
    }
}
