using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Components.Cells
{
    class PublicService : Property
    {
        #region Variables
        /// <summary>
        /// Public service's logo
        /// </summary>
        public string Logo { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creation of the cell "Public service"
        /// </summary>
        public PublicService() { }
        #endregion



    }
}
