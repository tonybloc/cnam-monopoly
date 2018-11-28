using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("PublicService")]
    public class PublicService : Property
    {
        #region Variables
        /// <summary>
        /// Public service's icon
        /// </summary>
        [XmlElement("Icon")]
        public string Icon { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of the cell "Public service"
        /// </summary>
        public PublicService() { }
        #endregion
    }
}
