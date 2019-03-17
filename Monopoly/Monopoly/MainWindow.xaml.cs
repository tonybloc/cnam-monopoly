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
using Monopoly.Handlers;
using Monopoly.Models.Components;
using Monopoly.View;
using Monopoly.View.Notifications.Dialog;

namespace Monopoly
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private GameManager _GameManager;

        
        public MainWindow()
        {          

            InitializeComponent();

            _GameManager = GameManager.Instance;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }
        

        private void btnCustomize_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ConfigurationInterface();
            MenuContent.Visibility = Visibility.Hidden;
        }

        private void btnPlayAlone_Click(object sender, RoutedEventArgs e)
        {
            _GameManager.SetType(GameManager.GameType.SINGLEPLAYER);
            MainContent.Content = new PageSinglePlayerCreation();
            MenuContent.Visibility = Visibility.Hidden;
        }

        private void btnLocalMultiplayer_Click(object sender, RoutedEventArgs e)
        {

            MainContent.Content = new PageMultiplePlayerCreation();
            MenuContent.Visibility = Visibility.Visible;
            _GameManager.SetType(GameManager.GameType.LOCAL_MUTLIPLAYER);
        }

        private void btnRules_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Escape the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                NotificationsPanel.Visibility = Visibility.Visible;
                NotificationsPanel.Content = new SubMenuOfGame();
            }

        }
    }
}
