using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("LandGroup")]
    public class LandGroup
    {
        #region Variables
        /// <summary>
        /// Land group's identifer
        /// </summary>
        [XmlElement("IdGroup")]
        public int IdGroup { get; set; }
        /// <summary>
        /// Land group's color
        /// </summary>
        [XmlElement("Color")]
        public string Color { get; set; }
        /// <summary>
        /// Hotel's price
        /// </summary>
        [XmlElement("HotelPrice")]
        public int HotelPrice { get; set; }
        /// <summary>
        /// House's price
        /// </summary>
        [XmlElement("HousePrice")]
        public int HousePrice { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of the group of lands
        /// </summary>
        public LandGroup() { }
        #endregion
    }
}
