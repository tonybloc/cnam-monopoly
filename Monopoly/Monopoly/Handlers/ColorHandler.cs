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

        #region Methode (public)
        /// <summary>
        /// Retourne la prochaine couleur
        /// </summary>
        /// <returns></returns>
        public string GetNextPawnColor()
        {
            _colorIndex = (_colorIndex + 1) % _pawnColors.Count;
            return _pawnColors[_colorIndex];
        }

        /// <summary>
        /// Retourne la couleur précedente
        /// </summary>
        /// <returns></returns>
        public string GetPreviousPawnColor()
        {
            _colorIndex = _colorIndex - 1;
            _colorIndex = (_colorIndex < 0) ? _pawnColors.Count - 1 : _colorIndex;
            return _pawnColors[_colorIndex];
        }
        #endregion
    }
}
