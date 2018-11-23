using Monopoly.Handlers;
using Monopoly.Models.Components;
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
    /// Logique d'interaction pour PageSinglePlayerCreation.xaml
    /// </summary>
    public partial class PageSinglePlayerCreation : Page
    {
        #region Variables
        private static ColorHandler colorHandler;
        private static GameManager gameManager;
        public Player player;
        public string ColorValue;
        #endregion

        public PageSinglePlayerCreation()
        {
            InitializeComponent();
        }

        private void onGotFocus_Pseudo(object sender, RoutedEventArgs e)
        {
            
        }

        private void onLostFocus_Pseudo(object sender, RoutedEventArgs e)
        {

        }


        private void onClickPreviousColor(object sender, RoutedEventArgs e)
        {
            
        }

        private void onClickNextColor(object sender, RoutedEventArgs e)
        {
            
        }

        private void onClickValidate(object sender, RoutedEventArgs e)
        {
            
        }

        private void onClickCancel(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).MainContent.Content = "";
            ((MainWindow)Window.GetWindow(this)).MenuContent.Visibility = Visibility.Visible;

        }
    }
}
