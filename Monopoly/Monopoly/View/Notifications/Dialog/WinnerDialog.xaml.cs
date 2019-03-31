using Monopoly.Handlers;
using System;
using System.Collections.Generic;
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

namespace Monopoly.View.Notifications.Dialog
{


    /// <summary>
    /// Logique d'interaction pour WinnerDialog.xaml
    /// </summary>
    public partial class WinnerDialog : Page, INotifyPropertyChanged
    {

        private string _playerName;
        public string PlayerName
        {
            get
            {
                return _playerName;
            }
            set
            {
                if (_playerName != value)
                {
                    _playerName = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public WinnerDialog()
        {
            InitializeComponent();
            this.DataContext = this;
            PlayerName = PlayerHandler.Instance.FindWiner().Name;
        }

        /// <summary>
        /// Notify the changement an property
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void onClick_ExitGame(object sender, EventArgs e)
        {
            this.Content = null;
            ((MainWindow)Window.GetWindow(this)).Close();
        }
    }
}
