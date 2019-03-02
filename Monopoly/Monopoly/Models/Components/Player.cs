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
        public const int BUILD_HOTEL = 1;
        public const int BUILD_HOUSE = 2;
        #endregion

        #region Variables
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public Pawn Pawn { get; set; }
        public int Position { get; set; }
        public List<Property> ListOfProperties { get; private set; }
        #endregion  

        #region Constructeurs
        /// <summary>
        /// Crée une instance de la classe
        /// </summary>
        public Player()
        {
            this.Id = 2;
            this.Name = "";
            this.Pawn = new Pawn();
            this.Position = 0;
            this.Status = WAITING;
            this.ListOfProperties = new List<Property>();
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
            this.ListOfProperties = new List<Property>();
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

        #region Methodes publique
        /// <summary>
        /// Ajoute une propriété à sa liste de biens
        /// </summary>
        /// <param name="property">Porpriete</param>
        public void AddPorperty(Property p)
        {
            this.ListOfProperties.Add(p);
        }

        /// <summary>
        /// Enlève une propriété à sa la liste de bien
        /// </summary>
        /// <param name="p">Propriété</param>
        public void RemoveProperty(Property p)
        {
            this.ListOfProperties.Remove(p);
        }
        #endregion
    }
}
