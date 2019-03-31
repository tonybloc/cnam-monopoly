using Monopoly.Handlers;
using Monopoly.Models.Components.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// Logique d'interaction pour MortgagedPropertiesListInterface.xaml
    /// </summary>
    public partial class MortgagedPropertiesListInterface : Page, INotifyPropertyChanged
    {
        private static PlayerHandler playerHandler;

        public new double Opacity = 1;

        public ObservableCollection<Property> ListOfMortgagedProperties { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MortgagedPropertiesListInterface()
        {
            InitializeComponent();            
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.DataContext = this;

            playerHandler = PlayerHandler.Instance;
            //this.horizontalListBox.ItemsSource = playerHandler.Properties();

            List<Property> properties = playerHandler.GetCurrentPlayer().ListOfProperties.Where(l => l.Status == Property.NOT_AVAILABLE_ON_SALE).ToList();
            List<Property> ListOtherProperties = properties.Where(l => l.GetType() != typeof(Land)).ToList();
            List<Property> ListOfLands = properties.Where(l => l is Land).Cast<Land>().Where(l => l.NbHouse == 0 && l.NbHotel == 0).OrderBy(l => l.LandGroup.IdGroup).Cast<Property>().ToList();

            var res = new ObservableCollection<Property>();
            ListOtherProperties.ForEach(p => res.Add(p));
            ListOfLands.ForEach(p => res.Add(p));
            res.OrderBy(p => p.Id);

            ListOfMortgagedProperties = res;
            ListOfMortgagedProperties.CollectionChanged += OnCollectionChanged;

            if (ListOfMortgagedProperties.Count == 0)
            {
                EmptyList.Visibility = Visibility.Visible;
            }
        }

        private void horizontalListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Property p = (Property)e.AddedItems[0];

            playerHandler.Mortgage(playerHandler.GetCurrentPlayer(), p);
        }

        #region Event Property Change
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ListOfMortgagedProperties");
        }
        #endregion
    }
}
