using Monopoly.Handlers;
using Monopoly.Models.Components.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class RaiseMortgagedPropertiesListInterface : Page, INotifyPropertyChanged
    {
        private static PlayerHandler playerHandler;

        public ObservableCollection<Property> ListOfRaiseMortgagedProperties { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RaiseMortgagedPropertiesListInterface()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.DataContext = this;

            playerHandler = PlayerHandler.Instance;
            ListOfRaiseMortgagedProperties = new ObservableCollection<Property>(playerHandler.GetCurrentPlayer().ListOfProperties.Where(l => l.Status == Property.MORTGAGED).ToList());

            if (ListOfRaiseMortgagedProperties.Count() == 0)
            {
                EmptyList.Visibility = Visibility.Visible;
            }
        }

        private void horizontalListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Property p = (Property)e.AddedItems[0];

            playerHandler.RaiseMortgage(playerHandler.GetCurrentPlayer(), p);
        }

        #region Event Property Change
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
