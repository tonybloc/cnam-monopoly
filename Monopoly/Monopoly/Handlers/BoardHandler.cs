using Monopoly.Models.Components;
using Monopoly.Models.Components.Cells;
using Monopoly.Service.Xml;
using Monopoly.Settings;

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
        private BoardHandler()
        {
            this.Board = XmlDataAccess.XMLDeserializeObject<Board>(Config.filePath_XmlBoard);
            
        }

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
        
       
        /// <summary>
        /// Returne the color of land groupe
        /// </summary>
        /// <param name="c">Cell</param>
        /// <returns></returns>
        public string getColor(Cell c)
        {
            if (c is Land)
            {
                Land terrain = (Land)c;
                return terrain.LandGroup.Color;
            }
            return "#000";

        }
        
    }
}
