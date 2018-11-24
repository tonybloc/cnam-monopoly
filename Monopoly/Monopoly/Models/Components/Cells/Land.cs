using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    class Land : Property
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
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Build a house on the land
        /// </summary>
        public void BuildHouse(int nbHouse)
        {
            if ((this.NbHouse + nbHouse) > 4)
            {
                BuildHotel();
            }
            else
            {
                this.NbHouse += nbHouse;
            }
        }

        /// <summary>
        /// Build a hotel on the land
        /// </summary>
        public void BuildHotel()
        {
            this.NbHouse = 0;
            this.NbHotel = 1;
        }

        /// <summary>
        /// Return the rent price
        /// </summary>
        /// <returns></returns>
        public int GetRental()
        {
            if (NbHotel > 0)
            {
                return RantalList[NbHotel + 4];
            }
            else
            {
                return RantalList[NbHouse];
            }
        }
        #endregion
    }
}
