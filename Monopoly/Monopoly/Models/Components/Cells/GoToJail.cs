using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    public class GoToJail : Cell
    {
        #region Variables
        /// <summary>
        /// Icon of the cell "Go to jail"
        /// </summary>
        public string Icon { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of the cell "Go to jail"
        /// </summary>
        public GoToJail() { }
        #endregion
    }
}
