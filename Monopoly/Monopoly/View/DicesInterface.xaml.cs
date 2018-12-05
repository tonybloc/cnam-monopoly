using Monopoly.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monopoly.View
{
    /// <summary>
    /// Logique d'interaction pour DicesInterface.xaml
    /// </summary>
    public partial class DicesInterface : Page
    {
        #region Variables
        private static GameManager gameManager;

        
        #endregion

        #region Constructeur
        public DicesInterface()
        {
            InitializeComponent();
            gameManager = GameManager.Instance;
            //Thread thread = new Thread(new ThreadStart(showDices));
            //thread.Start();

            showDices();
        }
        #endregion

        #region Methods

       /* private void removeGif()
        {        

            
        }*/

        private void showDices()
        {
            /*while (Thread.CurrentThread.IsAlive)
            {
                Thread.Sleep(3000);
            }*/
            //System.Timers.Timer timer = new System.Timers.Timer(5000);
            gameManager.RoolDice(gameManager.FirstDice, gameManager.SecondeDice);
            
            Result.Children.Remove(Gif);
            FirstDice.Source = new BitmapImage(new Uri("/Monopoly;component/Resources/Pictures/Dices/dice"+gameManager.FirstDice.Value+".png", UriKind.Relative));
            SecondDice.Source = new BitmapImage(new Uri("/Monopoly;component/Resources/Pictures/Dices/dice"+ gameManager.SecondeDice.Value + ".png", UriKind.Relative));            
        }
        #endregion

    }
}
