using Monopoly.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Handlers
{
    public class BoardHandler
    {
        #region Variables
        /// <summary>
        /// Board handler instance
        /// </summary>
        private static BoardHandler _instance = null;

        /// <summary>
        /// Instance of the board
        /// </summary>
        public Board Board { get; set; }

        /// <summary>
        /// Creation of the boardHandler
        /// </summary>
        private BoardHandler() {}

        /// <summary>
        /// Return board handler instance
        /// </summary>
        public static BoardHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BoardHandler();
                }
                return _instance;
            }
        }
        #endregion
    }
}
