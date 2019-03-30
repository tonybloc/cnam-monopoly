using Monopoly.View.Notifications.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Logique d'interaction pour RulesInterface.xaml
    /// </summary>
    public partial class RulesInterface : Page
    {
        private const string DisableScriptError =
    @"function noError() {
          return true;
       }       

       window.onerror = noError;";

        public RulesInterface()
        {
            try
            {

                InitializeComponent();
                clsWebbrowser_Errors.SuppressscriptErrors(webBrowser, true);
                this.webBrowser.Navigate("https://ludos.brussels/ludo-sesame/opac_css/doc_num.php?explnum_id=275");
            }
            catch(Exception e)
            {
                NotificationsPanel.Visibility = Visibility.Visible;
                NotificationsPanel.Content = new ExceptionDialog("Une connection Internet est requise.");
            }
        }
      
        private void ReturnToMenu(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).MainContent.Content = null;
            ((MainWindow)Window.GetWindow(this)).MenuContent.Visibility = Visibility.Visible;
            this.Content = null;
        }

       

    }
}

