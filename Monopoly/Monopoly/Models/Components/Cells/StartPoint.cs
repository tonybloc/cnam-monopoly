using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("StartPoint")]
    public class StartPoint : Cell
    {
        #region Variables
        /// <summary>
        /// Startpoint's Icon
        /// </summary>
        [XmlElement("Icon")]
        public string Icon { get; set; }
        /// <summary>
        /// Startpoint's description
        /// </summary>
        [XmlElement("Description")]
        public string Description { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of the cell "Startpoint"
        /// </summary>
        public StartPoint() { }
        #endregion
    }
}
