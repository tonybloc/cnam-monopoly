using Monopoly.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Tools
{
    public class Tools
    {
        /// <summary>
        /// Shuffle list of object
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="list">liste to shuffle</param>
        public static void Shuffle<T>(List<T> list)
        {
            Random rand = new Random();
            int nbMotion = list.Count;
            while (nbMotion > 1)
            {
                nbMotion--;
                int randomIndex = rand.Next(nbMotion + 1);
                T obj = list[randomIndex];
                list[randomIndex] = list[nbMotion];
                list[nbMotion] = obj;
            }

        }
    }
}
