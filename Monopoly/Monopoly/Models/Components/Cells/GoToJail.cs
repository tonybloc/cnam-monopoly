using System;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("GoToJail")]
    public class GoToJail : Cell
    {
        #region Variables
        /// <summary>
        /// Icon of the cell "Go to jail"
        /// </summary>
        [XmlElement("Icon")]
        public string Icon { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of the cell "Go to jail"
        /// </summary>
        public GoToJail() { }
        #endregion
    }
}
