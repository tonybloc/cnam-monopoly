using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    public class TrainStation : Property
    {
        #region Variables
        /// <summary>
        /// Train station's Icon
        /// </summary>
        public string Icon { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of a cell "Train station"
        /// </summary>
        public TrainStation() { }
        #endregion
    }
}
