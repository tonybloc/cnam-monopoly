using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    public class LandGroup
    {
        #region Variables
        /// <summary>
        /// Land group's identifer
        /// </summary>
        public int IdGroup { get; set; }

        /// <summary>
        /// Land group's color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Hotel's price
        /// </summary>
        public int HotelPrice { get; set; }

        /// <summary>
        /// House's price
        /// </summary>
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
