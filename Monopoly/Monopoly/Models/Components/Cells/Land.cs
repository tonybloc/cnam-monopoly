using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    public class Land : Property
    {
        #region Variables
        /// <summary>
        /// Group of lands
        /// </summary>
        public LandGroup LandGroup { get; set; }

        /// <summary>
        /// List of the different rents
        /// </summary>
        public List<int> RantalList { get; set; }

        /// <summary>
        /// Number of hotels
        /// </summary>
        public int NbHotel { get; private set; }

        /// <summary>
        /// Number of houses
        /// </summary>
        public int NbHouse { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of a cell "Land"
        /// </summary>
        public Land()
        {
            NbHouse = 0;
            NbHotel = 0;
            this.RantalList = new List<int>();
        }
        #endregion
    }
}
