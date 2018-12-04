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
using Monopoly.View;
namespace Monopoly
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPlayAlone_Click(object sender, RoutedEventArgs e)
        {
            //MainContent.Content = new PageSinglePlayerCreation();
            MainContent.Content = new DicesInterface();
            MenuContent.Visibility = Visibility.Hidden;
        }

        private void btnCustomize_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ConfigurationInterface();
            MenuContent.Visibility = Visibility.Hidden;
        }

        private void btnNetworkPlay_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnRules_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
