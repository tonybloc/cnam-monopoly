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
        /// Shuffle a list of player
        /// </summary>
        /// <param name="list"></param>
        public static void Shuffle(List<Player> list)
        {
            Random rand = new Random();
            int nbMotion = list.Count;
            while (nbMotion > 1)
            {
                nbMotion--;
                int randomIndex = rand.Next(nbMotion + 1);
                Player player = list[randomIndex];
                list[randomIndex] = list[nbMotion];
                list[nbMotion] = player;
            }

        }

        /// <summary>
        /// Shuffle a list of string
        /// </summary>
        /// <param name="list"></param>
        public static void Shuffle(List<string> list)
        {
            Random rand = new Random();
            int nbMotion = list.Count;
            while (nbMotion > 1)
            {
                nbMotion--;
                int randomIndex = rand.Next(nbMotion + 1);
                string s = list[randomIndex];
                list[randomIndex] = list[nbMotion];
                list[nbMotion] = s;
            }

        }
    }
}
