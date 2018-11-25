using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    public class StartPoint : Cell
    {
        #region Variables
        /// <summary>
        /// Startpoint's Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Startpoint's description
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of the cell "Startpoint"
        /// </summary>
        public StartPoint() { }
        #endregion
    }
}
