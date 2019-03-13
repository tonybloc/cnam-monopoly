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
        private GameManager _GameManager;
        private DicesHandler _DicesHandler;
        private PlayerHandler _PlayerHandler;

        DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
        DispatcherTimer secondTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
        #endregion

        #region Constructor
        public DicesInterface()
        {
            InitializeComponent();

            _GameManager = GameManager.Instance;
            _DicesHandler = DicesHandler.Instance;
            _PlayerHandler = PlayerHandler.Instance;

            ShowDices();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Display dices gif and the results
        /// </summary>
        private void ShowDices()
        {
            //Initialisation of the gif
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("/Monopoly;component/Resources/Pictures/Dices/RollingDice.gif", UriKind.Relative);
            image.EndInit();

            ImageBehavior.SetAnimatedSource(Gif, image);

            _DicesHandler.RoolDices(_PlayerHandler.GetCurrentPlayer());

            timer.Start();
            timer.Tick += Timer_Tick;
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            Result.Children.Remove(Gif);
            FirstDice.Source = new BitmapImage(new Uri("/Monopoly;component/Resources/Pictures/Dices/dice" + _DicesHandler.FirstDice.Value + ".png", UriKind.Relative));
            SecondDice.Source = new BitmapImage(new Uri("/Monopoly;component/Resources/Pictures/Dices/dice" + _DicesHandler.SecondDice.Value + ".png", UriKind.Relative));
            //secondTimer.Start();
            //secondTimer.Tick += SecondTime_Tick;
        }

        private void SecondTime_Tick(object sender, EventArgs e)
        {
            secondTimer.Stop();
            MessageBox.Show("Test");
            
        }
    }
    #endregion
}

