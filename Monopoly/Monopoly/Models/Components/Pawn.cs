using Monopoly.Resources.Colors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components
{
    public class Pawn : INotifyPropertyChanged
    {
        #region Variables
        /// <summary>
        /// Couleur du pion
        /// </summary>
        private string _colorValue;
        public string ColorValue
        {
            get { return _colorValue; }
            set
            {
                if(_colorValue != value)
                {
                    _colorValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public int X;
        public int Y;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructeurs
        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        public Pawn()
        {
            this.ColorValue = "#000000";
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

        #region NotifyPropertyChanged

        /// <summary>
        /// Notify Property Changed
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
