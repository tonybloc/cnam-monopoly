using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cards
{
    /// <summary>
    /// Paquet de cartes
    /// </summary>
    [Serializable]
    [XmlRoot("Deck")]
    public class Deck
    {
        [XmlElement("Card")]
        public List<Card> Cards { get; set; }

        [XmlIgnore]
        private int _index;

        /// <summary>
        /// Create new instance of class
        /// </summary>
        public Deck()
        {
            this.Cards = new List<Card>();
            _index = 0;
        }

        /// <summary>
        /// Récupère la prochaine carte du packet
        /// </summary>
        /// <returns>Carte</returns>
        public Card GetNextCard()
        {
            _index = (_index + 1) % Cards.Count;
            return Cards[_index];
        }
       

        public void DisplayCards()
        {
            foreach (Card c in Cards)
            {
                Console.WriteLine("Carte de type " + c.GetType().Name);
            }
        }

    }
}
