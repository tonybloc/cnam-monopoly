using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components
{
    public class Player
    {
        #region Variables
        public int Id { get; set; }
        public string Name { get; set; }
        public Pawn Pawn { get; set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        public Player()
        {
            this.Id = 1;
            this.Name = "";
            this.Pawn = new Pawn();
        }

        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        /// <param name="id">Id du joueur</param>
        /// <param name="name">Nom du joueur</param>
        /// <param name="pawn">Pion du joueur</param>
        public Player(int id, string name, Pawn pawn)
        {
            this.Id = id;
            this.Name = name;
            this.Pawn = pawn;
        }
        #endregion
    }
}
