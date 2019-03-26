 using Monopoly.Models.Components;
using Monopoly.Models.Components.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cards
{
    [Serializable]
    [XmlRoot("Card")]
    [XmlInclude(typeof(CardAnniversary))]
    [XmlInclude(typeof(CardChoice))]
    [XmlInclude(typeof(CardMove))]
    [XmlInclude(typeof(CardMoveToCell))]
    [XmlInclude(typeof(CardUpdateMoney))]
    [XmlInclude(typeof(CardUpdateMoneyAccordingBuilds))]
    [XmlInclude(typeof(CardMoveToJail))]
    [XmlInclude(typeof(CardExitToJail))]
    public abstract class Card
    {
        #region Variables

        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }
        
        [XmlElement("Type")]
        public int Type { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructeur
        /// <summary>
        /// Create new instance of class
        /// </summary>
        public Card() { }

        #endregion

        #region NotifyPropertyChanged

        /// <summary>
        /// Notify Property Changed
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
