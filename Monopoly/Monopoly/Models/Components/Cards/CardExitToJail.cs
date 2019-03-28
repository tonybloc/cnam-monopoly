using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cards
{
    [Serializable]
    [XmlRoot("CardExitToJail")]
    public class CardExitToJail : Card
    {
        [XmlIgnore]
        public ActionOption Action { get; set; }
        public enum ActionOption : int { NODEFINED = 0, EXITTOJAIL = 1, KEEPCARD = 2 };

        /// <summary>
        /// Create new instance of CardExitToJail
        /// </summary>
        public CardExitToJail() : base() { }
        
        
    }
}
