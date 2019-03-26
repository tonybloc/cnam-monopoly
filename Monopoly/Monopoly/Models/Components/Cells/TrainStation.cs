using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("TrainStation")]
    public class TrainStation : Property
    {
        #region Variables
        /// <summary>
        /// Train station's Icon
        /// </summary>
        [XmlElement("Icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Rent of the train station 
        /// </summary>
        [XmlIgnore]
        public int TrainStationRent { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of a cell "Train station"
        /// </summary>
        public TrainStation() {
            TrainStationRent = 25;
        }
        #endregion
    }
}
