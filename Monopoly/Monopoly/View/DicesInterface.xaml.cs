using Monopoly.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }
        #endregion

        #region Methods
        private void onClickValidate(object sender, RoutedEventArgs e)
        {
            gameManager.RoolDice(gameManager.FirstDice, gameManager.SecondeDice);

            Console.WriteLine("1:" + gameManager.FirstDice.Value);
            Console.WriteLine("2:" + gameManager.SecondeDice.Value);

            Result.Children.Remove(Gif);
            //BitmapImage bi = new BitmapImage();
            //bi.BeginInit();
            //string s = System.IO.Path.GetFullPath("dice1.png");
            //bi.UriSource = new Uri("../Resources/Pictures/Dices/dice1.png");
            //bi.EndInit();
            //FirstDice.Stretch = Stretch.Fill;
            FirstDice.Source = new BitmapImage(new Uri(@"C:\CNAM\projets\monopoly\Monopoly\Monopoly\Resources\Pictures\Dices\dice"+gameManager.FirstDice.Value+".png"));
            SecondDice.Source = new BitmapImage(new Uri(@"C:\CNAM\projets\monopoly\Monopoly\Monopoly\Resources\Pictures\Dices\dice" + gameManager.SecondeDice.Value + ".png"));

        }


        private string getPhotos(int value)
        {

            return null;
        }
        #endregion

    }
}
