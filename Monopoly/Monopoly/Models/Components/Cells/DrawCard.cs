using System;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("DrawCard")]
    public class DrawCard : Cell
    {
        #region Variables
        /// <summary>
        /// Draw card's icon
        /// </summary>
        [XmlElement("Icon")]
        public string Icon { get; set; }
        /// <summary>
        /// Type of draw card : chance or community
        /// </summary>
        [XmlElement("Type")]
        public int Type { get; set; }
        /// <summary>
        /// Type chance
        /// </summary>
       
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of a cell for the "Draw card"
        /// </summary>
        public DrawCard() { }
        #endregion
    }
}
