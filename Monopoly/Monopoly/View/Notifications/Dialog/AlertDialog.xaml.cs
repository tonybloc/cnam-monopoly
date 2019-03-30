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
        private TypeOfAlert _type;

        private string _BackgroundColor;
        public string BackgroundColor
        {
            get { return _BackgroundColor; }
            set
            {
                if(_BackgroundColor != value)
                {
                    _BackgroundColor = value;
                    OnPropertyChanged();
                }
            }
        }

        private static string WARNING_COLOR = "#FFAB91";
        private static string ERROR_COLOR = "#C62828";
        private static string SUCCES_COLOR = "#A5D6A7";
        private static string INFO_COLOR = "#64B5F6";

        public enum TypeOfAlert : int { WARNING = 0, ERROR = 1, SUCCES = 2, INFO = 3};

        /// <summary>
        /// Create new instance of class
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="t">type of alert</param>
        public AlertDialog(Exception ex, TypeOfAlert t)
        {
            InitializeComponent();
            DataContext = this;
            Message = ex.StackTrace.ToString();
            _type = t;
            switch(_type)
            {
                case TypeOfAlert.ERROR:
                    BackgroundColor = ERROR_COLOR;
                    break;
                case TypeOfAlert.WARNING:
                    BackgroundColor = WARNING_COLOR;
                    break;
                case TypeOfAlert.SUCCES:
                    BackgroundColor = SUCCES_COLOR;
                    break;
                case TypeOfAlert.INFO:
                    BackgroundColor = INFO_COLOR;
                    break;
            }
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(15) };
            _timer.Tick += new EventHandler(EventShowMessage);
            _timer.Start();
        }

        /// <summary>
        /// Create new instance of class
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="t">type of alert</param>
        public AlertDialog(string message, TypeOfAlert t)
        {
            InitializeComponent();
            DataContext = this;
            Message = message;
            _type = t;
            switch (_type)
            {
                case TypeOfAlert.ERROR:
                    BackgroundColor = ERROR_COLOR;
                    break;
                case TypeOfAlert.WARNING:
                    BackgroundColor = WARNING_COLOR;
                    break;
                case TypeOfAlert.SUCCES:
                    BackgroundColor = SUCCES_COLOR;
                    break;
                case TypeOfAlert.INFO:
                    BackgroundColor = INFO_COLOR;
                    break;
            }

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(15) };
            _timer.Tick += new EventHandler(EventShowMessage);
            _timer.Start();
        }

        /// <summary>
        /// Create new instance of class
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="t">type of alert</param>
        /// <param name="d">dalay to display alert</param>
        public AlertDialog(Exception ex, TypeOfAlert t, TimeSpan d)
        {
            InitializeComponent();
            DataContext = this;
            Message = ex.Message;
            _type = t;
            switch (_type)
            {
                case TypeOfAlert.ERROR:
                    BackgroundColor = ERROR_COLOR;
                    break;
                case TypeOfAlert.WARNING:
                    BackgroundColor = WARNING_COLOR;
                    break;
                case TypeOfAlert.SUCCES:
                    BackgroundColor = SUCCES_COLOR;
                    break;
                case TypeOfAlert.INFO:
                    BackgroundColor = INFO_COLOR;
                    break;
            }
            this.Opacity = 0;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += new EventHandler(EventShowMessage);
            _timer.Start();
        }

        /// <summary>
        /// Create new instance of class
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="t">type of alert</param>
        /// <param name="d">delay to display the alert </param>
        public AlertDialog(string message, TypeOfAlert t, TimeSpan d)
        {
            InitializeComponent();
            DataContext = this;
            Message = message;
            _type = t;
            switch (_type)
            {
                case TypeOfAlert.ERROR:
                    BackgroundColor = ERROR_COLOR;
                    break;
                case TypeOfAlert.WARNING:
                    BackgroundColor = WARNING_COLOR;
                    break;
                case TypeOfAlert.SUCCES:
                    BackgroundColor = SUCCES_COLOR;
                    break;
                case TypeOfAlert.INFO:
                    BackgroundColor = INFO_COLOR;
                    break;
            }

            this.Opacity = 0;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += new EventHandler(EventShowMessage);
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

        private void EventShowMessage(object sender, EventArgs e)
        {
            double x = 0.05;
            if (this.Opacity <= 0.95)
            {
                _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                this.Opacity += x;
            }
            else
            {
                _timer.Interval = new TimeSpan(0, 0, 3);
                _timer.Tick += new EventHandler(EventHiddeMessage);
            }
        }

        private void EventHiddeMessage(object sender, EventArgs e)
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

        public void Clear()
        {
            _timer.Stop();
            this.Content = null;
        }

        
    }
}
