using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    class StartPoint : Cell
    {
        #region Variables
        /// <summary>
        /// Startpoint's logo
        /// </summary>
        public string Logo { get; set; }

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
