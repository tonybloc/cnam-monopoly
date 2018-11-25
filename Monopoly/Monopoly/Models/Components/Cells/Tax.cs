using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    public class Tax : Cell
    {
        #region Variables
        /// <summary>
        /// Tax's Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Tax's amount
        /// </summary>
        public int Amount { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of the cell "Tax"
        /// </summary>
        public Tax() { }
        #endregion
    }
}
