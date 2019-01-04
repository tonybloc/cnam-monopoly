using Monopoly.Models.Components.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components
{
    public class Player
    {
        #region Constantes
        public const int WAITING = 0;
        public const int PLAYING = 1;
        #endregion

        #region Variables
        public int Id { get; set; }
        public string Name { get; set; }
        public Pawn Pawn { get; set; }
        public int Position { get; set; }
        public int Status { get; set; }
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
            this.Position = 0;
            this.Status = WAITING;
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
            this.Position = 0;
            this.Status = WAITING;
        }

        /// <summary>
        /// Deplace le joueurs à la position cible
        /// </summary>
        /// <param name="nb">Nombre de deplacement</param>
        public void MoveTo(int position)
        {
            this.Position = position;
        }

        /// <summary>
        /// Deplace le joueur sur une case cible
        /// </summary>
        /// <param name="cell">Case du plateau</param>
        public void MoveTo(Cell cell)
        {
            this.Position = cell.Id;
        }
        #endregion
    }
}
