using System;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("Cell")]
    [XmlInclude(typeof(DrawCard))]
    [XmlInclude(typeof(GoToJail))]
    [XmlInclude(typeof(Jail))]
    [XmlInclude(typeof(Property))]
    [XmlInclude(typeof(Parking))]
    [XmlInclude(typeof(StartPoint))]
    [XmlInclude(typeof(Tax))]
    public class Cell
    {
        #region Variables
        /// <summary>
        /// Cell's identifier 
        /// </summary>
        [XmlElement("Id")]
        public int Id { get; set; }
        /// <summary>
        /// Cell's title
        /// </summary>
        [XmlElement("Title")]
        public string Title { get; set; }
        #endregion

        #region Constructeur
        /// <summary>
        /// Creation of the cell
        /// </summary>
        public Cell() { }
        #endregion
    }
}
