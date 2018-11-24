using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    public class Cell
    {
        #region Variables
        /// <summary>
        /// Cell's identifier 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Cell's title
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Constructeur
        /// <summary>
        /// Creation of the cell
        /// </summary>
        public Cell() { }
        #endregion
    }
}
