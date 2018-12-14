using Monopoly.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

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

        #region Constructor
        public DicesInterface()
        {
            InitializeComponent();
            gameManager = GameManager.Instance;
            showDices();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Display dices gif and the results
        /// </summary>
        private void showDices()
        {
            //Initialisation of the gif
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("/Monopoly;component/Resources/Pictures/Dices/RollingDice.gif", UriKind.Relative);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Gif, image);

            // Execute the gif during 2 seconds and then display the result
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            timer.Start();
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                Result.Children.Remove(Gif);
                gameManager.RoolDice(gameManager.FirstDice, gameManager.SecondeDice);
                FirstDice.Source = new BitmapImage(new Uri("/Monopoly;component/Resources/Pictures/Dices/dice" + gameManager.FirstDice.Value + ".png", UriKind.Relative));
                SecondDice.Source = new BitmapImage(new Uri("/Monopoly;component/Resources/Pictures/Dices/dice" + gameManager.SecondeDice.Value + ".png", UriKind.Relative));
            };
        }
    }
    #endregion
}

