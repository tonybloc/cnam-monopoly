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
    /// Logique d'interaction pour ExceptionDialog.xaml
    /// </summary>
    public partial class ExceptionDialog : UserControl, INotifyPropertyChanged
    {

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if(_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Create a new instance of class
        /// </summary>
        /// <param name="exp"></param>
        public ExceptionDialog(Exception exp) : base()
        {
            InitializeComponent();
            DataContext = this;
            Message = exp.Message;
        }

        /// <summary>
        /// Create a new instance of class
        /// </summary>
        /// <param name="exp"></param>
        public ExceptionDialog(string message) : base()
        {
            InitializeComponent();
            DataContext = this;
            Message = message;
        }
        /// <summary>
        /// Evenement of click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;

        }

        /// <summary>
        /// Notify the changement an property
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
