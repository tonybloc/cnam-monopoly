using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("Parking")]
    public class Parking : Cell
    {
        #region Variables
        /// <summary>
        /// Parking's icon
        /// </summary>
        [XmlElement("Icon")]
        public string Icon { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of a cell "Parking"
        /// </summary>
        public Parking() { }
        #endregion
    }
}
