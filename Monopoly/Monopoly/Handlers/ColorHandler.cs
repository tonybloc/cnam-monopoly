using Monopoly.Resources.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Handlers
{
    public class ColorHandler
    {
        #region Variables
        private static ColorHandler _instance = null;
        private static List<string> _pawnColors;
        private static int _colorIndex;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Création d'une instance de la classe
        /// </summary>
        private ColorHandler()
        {
            _pawnColors = PawnColors.Colors;
            _colorIndex = 0;
        }

        /// <summary>
        /// Instance de la classe
        /// </summary>
        public static ColorHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ColorHandler();
                }
                return _instance;
            }
        }
        #endregion
        
    }
}
