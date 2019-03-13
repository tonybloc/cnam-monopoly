using Monopoly.Models.Components.Cells;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Monopoly.Models.Components
{
    [Serializable]
    [XmlRoot("Board")]
    public class Board
    {
        #region Variables
        /// <summary>
        /// Instance of the board
        /// </summary>
        [XmlIgnore]
        private static Board _instance = null;

        /// <summary>
        /// List of cells on the board
        /// </summary>
        [XmlElement("Cell")]
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

        #region Methods
        /// <summary>
        /// Get cell thanks to the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Cell identified</returns>
        public Cell GetCell(int index)
        {
            Predicate<Cell> filtreCell = (Cell c) => { return c.Id == index; };
            return ListCell.Find(filtreCell);
        }
        #endregion
    }
}
