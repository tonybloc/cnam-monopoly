using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    public class Parking : Cell
    {
        #region Variables
        /// <summary>
        /// Parking's icon
        /// </summary>
        public string Icon { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of a cell "Parking"
        /// </summary>
        public Parking() { }
        #endregion
    }
}
