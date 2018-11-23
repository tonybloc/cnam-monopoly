using Monopoly.Resources.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components
{
    public class Pawn
    {
        #region Variables
        /// <summary>
        /// Couleur du pion
        /// </summary>
        public string ColorValue { get; set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        public Pawn()
        {
            this.ColorValue = "#000";
        }

        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        /// <param name="colorName">Index de la couleur</param>
        public Pawn(int colorIndex)
        {
            this.ColorValue = ((colorIndex < PawnColors.Colors.Count) && (colorIndex > 0)) ? PawnColors.Colors[colorIndex] : PawnColors.Colors[PawnColors.RED];
        }

        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        /// <param name="colorvalue">Valeur (héxadécimal) de la couleur</param>
        public Pawn(string colorvalue)
        {
            this.ColorValue = colorvalue;
        }
        #endregion
    }
}
