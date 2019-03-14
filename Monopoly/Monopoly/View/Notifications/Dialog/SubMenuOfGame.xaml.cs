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

namespace Monopoly.View.Notifications.Dialog
{
    /// <summary>
    /// Logique d'interaction pour SubMenuOfGame.xaml
    /// </summary>
    public partial class SubMenuOfGame : UserControl
    {
        public SubMenuOfGame()
        {
            InitializeComponent();
        }

        public void onClick_btnRefuse(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        public void onClick_btnAccepte(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).Close();

        }
    }
}
