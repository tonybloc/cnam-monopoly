using Monopoly.Handlers;
using Monopoly.Models.Components.Cells;
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
    /// Logique d'interaction pour RaiseMortgagedPropertiesListInterface.xaml
    /// </summary>
    public partial class RaiseMortgagedPropertiesListInterface : Page
    {
        private static PlayerHandler playerHandler;

        public RaiseMortgagedPropertiesListInterface()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            playerHandler = PlayerHandler.Instance;
            this.horizontalListBox.ItemsSource = playerHandler.MortagedLands();

            if (playerHandler.MortagedLands().Count() == 0)
            {
                EmptyList.Visibility = Visibility.Visible;
            }
        }

        private void horizontalListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Property p = (Property)e.AddedItems[0];
            playerHandler.RaiseMortgage(playerHandler.GetCurrentPlayer(), p);
        }
    }
}
