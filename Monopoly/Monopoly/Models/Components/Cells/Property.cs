using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    public class Property : Cell
    {
        #region Constants
        /// <summary>
        /// Constant to show if the property can be sold 
        /// </summary>
        public const int AVAILABLE_ON_SALE = 1;

        /// <summary>
        /// Constant to show that the property has been sold
        /// </summary>
        public const int NOT_AVAILABLE_ON_SALE = 2;
        #endregion

        #region Variables
        /// <summary>
        /// Property's status
        /// </summary>
        public int status;

        /// <summary>
        /// Property's purchase price
        /// </summary>
        public int PurchasePrice { get; set; }

        /// <summary>
        /// Property's mortgage price
        /// </summary>
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
