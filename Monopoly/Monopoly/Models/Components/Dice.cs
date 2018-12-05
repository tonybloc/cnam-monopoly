using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components
{
    public class Dice
    {
        private int _value;
        private int _nbFace;
        private static Random rand = new Random();

        /// <summary>
        /// Création d'une instance de la classe
        /// </summary>
        public Dice()
        {
            this._nbFace = 7;
            this._value = 0;
        }

        /// <summary>
        /// Retourne la valeur du dée
        /// </summary>
        public int Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Lance le dée
        /// </summary>
        public void Rool()
        {            
            _value = rand.Next(1, _nbFace);
        }
    }
}
