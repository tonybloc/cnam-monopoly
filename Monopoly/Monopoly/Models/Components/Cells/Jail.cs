using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("Jail")]
    public class Jail : Cell
    {
        #region Variables
        /// <summary>
        /// Icon of the cell "Jail"
        /// </summary>
        [XmlElement("Icon")]
        public string Icon { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Creation of the cell "Jail"
        /// </summary>
        public Jail() { }
        #endregion
    }
}
