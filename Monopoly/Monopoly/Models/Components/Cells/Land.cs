using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Monopoly.Models.Components.Cells
{
    [Serializable]
    [XmlRoot("Land")]
    public class Land : Property
    {
        #region Variables
        /// <summary>
        /// Group of lands
        /// </summary>
        [XmlElement("LandGroup")]
        public LandGroup LandGroup { get; set; }
        /// <summary>
        /// List of the different rents
        /// </summary>
        [XmlArray("RantalList")]
        [XmlArrayItem("Rantal")]
        public List<int> RantalList { get; set; }
        /// <summary>
        /// Number of hotels
        /// </summary>
        [XmlIgnore]
        public int NbHotel { get; private set; }
        /// <summary>
        /// Number of houses
        /// </summary>
        [XmlIgnore]
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

        #region Public methods
        public void AddHouse()
        {
            if (NbHouse < 4)
                this.NbHouse++;
        }

        public void RemoveHouse()
        {
            if (NbHouse > 0)
                this.NbHouse--;
        }

        public void AddHotel()
        {
            if (NbHouse == 4 && NbHotel < 1 )
                this.NbHotel++;
        }

        public void RemoveHotel()
        {
            if (NbHouse == 4 && NbHotel > 0)
                this.NbHotel--;
        }

        public int GetRent(int nb)
        {
            return RantalList[nb];
        }

        #endregion


    }
}
