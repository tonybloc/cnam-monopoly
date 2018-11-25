using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    public class DrawCard : Cell
    {
        #region Variables
        /// <summary>
        /// Draw card's icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Type of draw card : chance or community
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Type chance
        /// </summary>
        public const int TYPE_CHANCE = 0;

        /// <summary>
        /// Type community
        /// </summary>
        public const int TYPE_COMMUNITY_SERVICE = 1;
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of a cell for the "Draw card"
        /// </summary>
        public DrawCard() { }
        #endregion
    }
}
