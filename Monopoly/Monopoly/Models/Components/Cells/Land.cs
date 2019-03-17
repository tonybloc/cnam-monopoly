using System;
using System.Collections.Generic;
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
    public class Land : Property, INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

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

        private int NbHouseValue = 0;
        private int NbHotelValue = 0;

        /// <summary>
        /// Number of hotels
        /// </summary>
        [XmlIgnore]
        public int NbHotel
        {
            get
            {
                return this.NbHotelValue;
            }
            private set
            {
                if(value != this.NbHotelValue)
                {
                    this.NbHotelValue = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Number of houses
        /// </summary>
        [XmlIgnore]
        public int NbHouse {
            get
            {
                return this.NbHouseValue;
            }
            private set
            {
                if (value != this.NbHouseValue)
                {
                    this.NbHouseValue = value;
                    NotifyPropertyChanged();
                }
            }
        }
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

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Public methods
        public void AddHouse()
        {
            this.NbHouse++;
        }

        public void RemoveHouse()
        {
            if (NbHouse > 0)
                this.NbHouse--;
        }

        public void AddHotel()
        {
            this.NbHotel++;
            this.NbHouse = 0;
        }

        public void RemoveHotel()
        {
            this.NbHotel--;
            this.NbHouse = NB_MAX_HOUSES;
        }

        public int GetRent(int nb)
        {
            return RantalList[nb];
        }

        #endregion


    }
}
