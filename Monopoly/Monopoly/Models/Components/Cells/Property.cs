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
    [XmlRoot("Property")]
    [XmlInclude(typeof(Land))]
    [XmlInclude(typeof(PublicService))]
    [XmlInclude(typeof(TrainStation))]
    public class Property : Cell, INotifyPropertyChanged
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

        private int _status;
        [XmlElement("Status")]
        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }

        }

        private int _purchasePrice;
        [XmlElement("PurchasePrice")]
        public int PurchasePrice
        {
            get
            {
                return _purchasePrice;
            }
            set
            {
                if (_purchasePrice != value)
                {
                    _purchasePrice = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _mortgagePrice;
        [XmlElement("MortgagePrice")]
        public int MortgagePrice
        {
            get
            {
                return _mortgagePrice;
            }
            set
            {
                if (_mortgagePrice != value)
                {
                    _mortgagePrice = value;
                    OnPropertyChanged();
                }
            }
        }

        
        [XmlIgnore]
        private Player _owner;
        public Player Owner
        {
            get { return _owner; }
            set
            {
                if(_owner != value)
                {
                    _owner = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Create new instance of class
        /// </summary>
        public Property()
        {
            Status = AVAILABLE_ON_SALE;
            PurchasePrice = 0;
            MortgagePrice = 0;
            Owner = null;
        }

        #endregion

        #region NotifyPropertyChanged

        /// <summary>
        /// Notify Property Changed
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public override string ToString()
        {
            return this.Title;
        }
    }
}
