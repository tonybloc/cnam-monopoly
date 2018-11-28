using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("Tax")]
    public class Tax : Cell
    {
        #region Variables
        /// <summary>
        /// Tax's Icon
        /// </summary>
        [XmlElement("Icon")]       
        public string Icon { get; set; }
        /// <summary>
        /// Tax's amount
        /// </summary>
        [XmlElement("Amount")]
        public int Amount { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of the cell "Tax"
        /// </summary>
        public Tax() { }
        #endregion
    }
}
