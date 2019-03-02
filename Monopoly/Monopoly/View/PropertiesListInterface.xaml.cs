using Monopoly.Handlers;
using Monopoly.Models.Components.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour PropertiesListInterface.xaml
    /// </summary>
    public partial class PropertiesListInterface : Page
    {
        private static PlayerHandler playerHandler;


        List<Property> m_selectedEquipmentHorizontal = new List<Property>();

        public PropertiesListInterface()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            playerHandler = PlayerHandler.Instance;
            this.horizontalListBox.ItemsSource = playerHandler.GetCurrentPlayer().ListOfProperties;
            this.horizontalSelectedItemsListBox.ItemsSource = m_selectedEquipmentHorizontal;
        }
                     
        private void horizontalListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //playerHandler.BuildOnLand(playerHandler.GetCurrentPlayer(), (Land)e.AddedItems);
            if (e.AddedItems.Count > 0)
            {
                foreach (Property item in e.AddedItems)
                {
                    m_selectedEquipmentHorizontal.Add(item);
                    
                }
                
            }

           /*if (e.RemovedItems.Count > 0)
            {
                foreach (Property item in e.RemovedItems)
                {
                    m_selectedEquipmentHorizontal.Remove(item);
                }
            */
        }
    }
}
