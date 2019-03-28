using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        /// Constant of the maximum of houses per land
        /// </summary>
        [XmlIgnore]
        public const int NB_MAX_HOUSES = 4;
        /// <summary>
        /// Constant of the maximum of hotel per land
        /// </summary>
        [XmlIgnore]
        public const int NB_MAX_HOTEL = 1;
        

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
   
        private int _NbHotelValue;
        [XmlIgnore]
        public int NbHotel
        {
            get
            {
                return this._NbHotelValue;
            }
            private set
            {
                if(value != this._NbHotelValue)
                {
                    this._NbHotelValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _NbHouseValue;
        [XmlIgnore]
        public int NbHouse {
            get
            {
                return this._NbHouseValue;
            }
            private set
            {
                if (value != this._NbHouseValue)
                {
                    this._NbHouseValue = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Create new instance of class
        /// </summary>
        public Land()
        {
            NbHouse = 0;
            NbHotel = 0;
            this.RantalList = new List<int>();
        }

        #endregion
 
        #region Public methods

        /// <summary>
        /// Add House to Land
        /// </summary>
        public void AddHouse()
        {
            this.NbHouse++;
        }

        /// <summary>
        /// Remove House to Land
        /// </summary>
        public void RemoveHouse()
        {
            if (NbHouse > 0)
                this.NbHouse--;
        }

        /// <summary>
        /// Add Hotel to Land
        /// </summary>
        public void AddHotel()
        {
            this.NbHotel++;
            this.NbHouse = 0;
        }

        /// <summary>
        /// Remove Hotel to Land
        /// </summary>
        public void RemoveHotel()
        {
            this.NbHotel--;
            this.NbHouse = NB_MAX_HOUSES;
        }

        /// <summary>
        /// Return Rent of Land
        /// </summary>
        /// <param name="nb">Number of House / Hotel</param>
        /// <returns></returns>
        public int GetRent(int nbBulding)
        {
            return RantalList[nbBulding];
        }

        #endregion


    }
}
