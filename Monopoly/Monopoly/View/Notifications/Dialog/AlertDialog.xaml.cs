using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Monopoly.View.Notifications.Dialog
{
    /// <summary>
    /// Logique d'interaction pour AlertDialog.xaml
    /// </summary>
    public partial class AlertDialog : UserControl, INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        private DispatcherTimer _timer;

        /// <summary>
        /// Create new instance of class
        /// </summary>
        /// <param name="ex">Exception to show</param>
        public AlertDialog(Exception ex)
        {
            InitializeComponent();
            DataContext = this;
            Message = ex.Message;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            _timer.Tick += new EventHandler(EventDuration);
            _timer.Start();
        }

        /// <summary>
        /// Create new instance of class
        /// </summary>
        /// <param name="message">message to show</param>
        public AlertDialog(string message)
        {
            InitializeComponent();
            DataContext = this;

            this.Opacity = 1;
            Message = message;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void EventDuration(object sender, EventArgs e)
        {
            double x = 0.05;
            if(this.Opacity <= 0)
            {
                _timer.Stop();
                this.Content = null;
            }
            else
            {
                _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                this.Opacity -= x;
            }
            
        }
    }
}
