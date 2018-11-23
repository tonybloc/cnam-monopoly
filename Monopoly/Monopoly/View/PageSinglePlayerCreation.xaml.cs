using Monopoly.Handlers;
using Monopoly.Models.Components;
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
    /// Logique d'interaction pour PageSinglePlayerCreation.xaml
    /// </summary>
    public partial class PageSinglePlayerCreation : Page
    {
        #region Variables
        private static ColorHandler colorHandler;
        private static GameManager gameManager;
        private const string placeholder = "Your Pseudo";
        public string ColorValue;
        #endregion

        public PageSinglePlayerCreation()
        {
            InitializeComponent();
            colorHandler = ColorHandler.Instance;
            gameManager = GameManager.Instance;
        }

        private void onGotFocus_Pseudo(object sender, RoutedEventArgs e)
        {
            TextBox_Pseudo.Text = "";
        }

        private void onLostFocus_Pseudo(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox_Pseudo.Text))
            {
                TextBox_Pseudo.Text = placeholder;
            }
        }


        private void onClickPreviousColor(object sender, RoutedEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            ColorValue = colorHandler.GetNextPawnColor();
            PawnIcon.Fill = (Brush)bc.ConvertFrom(ColorValue);
        }

        private void onClickNextColor(object sender, RoutedEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            ColorValue = colorHandler.GetPreviousPawnColor();
            PawnIcon.Fill = (Brush)bc.ConvertFrom(ColorValue);
        }

        private void onClickValidate(object sender, RoutedEventArgs e)
        {
            if(this.TextBox_Pseudo.Text != placeholder)
            {
                gameManager.CreatePlayer(this.TextBox_Pseudo.Text, this.ColorValue);
            }            
        }

        private void onClickCancel(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).MainContent.Content = "";
            ((MainWindow)Window.GetWindow(this)).MenuContent.Visibility = Visibility.Visible;

        }
    }
}
