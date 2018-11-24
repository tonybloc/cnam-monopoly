using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    class TrainStation : Property
    {
        #region Variables
        /// <summary>
        /// Train station's logo
        /// </summary>
        public string Logo { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of a cell "Train station"
        /// </summary>
        public TrainStation() { }
        #endregion
    }
}
