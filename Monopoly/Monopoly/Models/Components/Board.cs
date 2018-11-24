using Monopoly.Models.Components.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components
{
    public class Board
    {
        #region Variables
        /// <summary>
        /// Instance of the board
        /// </summary>
        private static Board _instance = null;

        /// <summary>
        /// List of cells on the board
        /// </summary>
        public List<Cell> ListCell { get; set; }
        #endregion

        #region Constructeur
        /// <summary>
        /// Creation of the board
        /// </summary>
        private Board()
        {
            this.ListCell = new List<Cell>();
        }
        
        /// <summary>
        /// Return the unique instance of the class
        /// </summary>
        public static Board Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Board();
                }
                return _instance;
            }
        }
        #endregion
    }
}
