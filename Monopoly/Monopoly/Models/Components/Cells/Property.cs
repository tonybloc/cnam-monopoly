using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("Property")]
    [XmlInclude(typeof(Land))]
    [XmlInclude(typeof(PublicService))]
    [XmlInclude(typeof(TrainStation))]
    public class Property : Cell
    {
        #region Constants
        /// <summary>
        /// Constant to show if the property can be sold 
        /// </summary>
        [XmlIgnore]
        public const int AVAILABLE_ON_SALE = 0;
        /// <summary>
        /// Constant to show that the property has been sold
        /// </summary>
        [XmlIgnore]
        public const int NOT_AVAILABLE_ON_SALE = 1;
        /// <summary>
        /// Constant to show that the propery has been mortgaged
        /// </summary>
        [XmlIgnore]
        public const int MORTGAGED = 2;
        #endregion

        #region Variables
        /// <summary>
        /// Property's status
        /// </summary>
        [XmlElement("Status")]
        public int status;
        /// <summary>
        /// Property's purchase price
        /// </summary>
        [XmlElement("PurchasePrice")]
        public int PurchasePrice { get; set; }
        /// <summary>
        /// Property's mortgage price
        /// </summary>
        [XmlElement("MortgagePrice")]
        public int MortgagePrice { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of the cell "Property"
        /// </summary>
        public Property()
        {
            status = AVAILABLE_ON_SALE;
        }
        #endregion
    }
}
