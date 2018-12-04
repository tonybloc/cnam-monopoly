using Monopoly.Handlers;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Cells;
using Monopoly.Service;
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
    /// Logique d'interaction pour PageBoard.xaml
    /// </summary>
    public partial class PageBoard : Page
    {

        #region Variables
        public List<Player> players;
        private const int ORIENTATION_TOP = 1;
        private const int ORIENTATION_RIGHT = 2;
        private const int ORIENTATION_BOTTOM = 3;
        private const int ORIENTATION_LEFT = 4;

        private const int GRIDTYPE_ROW = 1;
        private const int GRIDTYPE_COLUMN = 2;
        #endregion


        public PageBoard()
        {
            InitializeComponent();
            InitialiseBoard();
        }

        #region Creation du Plateau

        /// <summary>
        /// Define the GridColumnDefinitions and GridRowDefinitions foreach panel in view
        /// </summary>
        /// <param name="panel">Panel</param>
        /// <param name="numOfDefinition">Iteration of row/column</param>
        /// <param name="type">Row or Column</param>
        private void initialisePanel(Grid panel, int numOfDefinition, int type)
        {
            switch (type)
            {
                case GRIDTYPE_COLUMN:
                    int nbCol = 0;
                    while (nbCol < numOfDefinition)
                    {
                        ColumnDefinition col = new ColumnDefinition();
                        panel.ColumnDefinitions.Add(col);
                        nbCol++;
                    }
                    break;
                case GRIDTYPE_ROW:
                    int nbRow = 0;
                    while (nbRow < numOfDefinition)
                    {
                        RowDefinition row = new RowDefinition();
                        panel.RowDefinitions.Add(row);
                        nbRow++;
                    }
                    break;
            }

        }

        /// <summary>
        /// Define elements in board
        /// </summary>
        private void InitialiseBoard()
        {
            List<Cell> BoardCells = GameManager.Instance.boardHandler.Board.ListCell;
            int index = 0;
            if ((BoardCells.Count >= 8) && (BoardCells.Count % 4 == 0))
            {
                int numberOfCellsToInsertInPanel = (int)((BoardCells.Count - 4) / 4);
                initialisePanel(BoardPanelTop, numberOfCellsToInsertInPanel, GRIDTYPE_COLUMN);
                initialisePanel(BoardPanelRight, numberOfCellsToInsertInPanel, GRIDTYPE_ROW);
                initialisePanel(BoardPanelLeft, numberOfCellsToInsertInPanel, GRIDTYPE_ROW);
                initialisePanel(BoardPanelBottom, numberOfCellsToInsertInPanel, GRIDTYPE_COLUMN);

                int indexOfNumberOfCellInPanel = 0;
                int globalIndex = 0;
                foreach (Cell c in BoardCells)
                {
                    globalIndex++;
                    if ((c.Id % (numberOfCellsToInsertInPanel + 1)) == 0)
                    {
                        index++;
                        indexOfNumberOfCellInPanel = 0;
                        switch (index)
                        {
                            case 1:
                                if (c.GetType() == typeof(StartPoint))
                                {
                                    Grid gridLayout = new Grid();
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });

                                    Image imgStart = new Image();
                                    imgStart.Source = Base64Converter.base64ToImageSource(((StartPoint)c).Icon);
                                    Grid.SetRow(imgStart, 0);

                                    TextBlock lbStart = new TextBlock();
                                    Grid.SetRow(lbStart, 2);

                                    gridLayout.Children.Add(imgStart);
                                    gridLayout.Children.Add(lbStart);

                                    BoardPanelStart.Children.Add(gridLayout);
                                    break;
                                }
                                break;
                            case 2:
                                if (c.GetType() == typeof(Jail))
                                {
                                    Grid gridLayout = new Grid();
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });

                                    RotateTransform rotate = new RotateTransform();
                                    rotate.Angle = 45;

                                    TransformGroup transform = new TransformGroup();
                                    transform.Children.Add(rotate);

                                    Image imgJail = new Image();                                    
                                    imgJail.Source = Base64Converter.base64ToImageSource(((Jail)c).Icon);
                                    imgJail.RenderTransformOrigin = new Point(0.5, 0.5);
                                    imgJail.RenderTransform = transform;

                                    Grid.SetRow(imgJail, 0);

                                    TextBlock lbJail = new TextBlock();
                                    Grid.SetRow(lbJail, 2);

                                    gridLayout.Children.Add(imgJail);
                                    gridLayout.Children.Add(lbJail);

                                    BoardPanelJail.Children.Add(gridLayout);
                                    break;
                                }
                                break;
                            case 3:
                                if (c.GetType() == typeof(Parking))
                                {
                                    Grid gridLayout = new Grid();
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });

                                    Image imgParking = new Image();
                                    imgParking.Source = Base64Converter.base64ToImageSource(((Parking)c).Icon);

                                    imgParking.RenderTransformOrigin = new Point(0.5, 0.5);
                                    ScaleTransform flipTrans = new ScaleTransform();
                                    imgParking.RenderTransform = flipTrans;
                                    Grid.SetRow(imgParking, 0);

                                    TextBlock lbParking = new TextBlock();
                                    Grid.SetRow(lbParking, 2);

                                    gridLayout.Children.Add(imgParking);
                                    gridLayout.Children.Add(lbParking);

                                    BoardPanelParking.Children.Add(gridLayout);
                                    break;
                                }
                                break;
                            case 4:
                                if(c.GetType() == typeof(GoToJail))
                                {
                                    Grid gridLayout = new Grid();
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });

                                    Image imgPolice = new Image();
                                    imgPolice.Source = Base64Converter.base64ToImageSource(((GoToJail)c).Icon);

                                    imgPolice.RenderTransformOrigin = new Point(0.5, 0.5);
                                    ScaleTransform flipTrans = new ScaleTransform();
                                    flipTrans.ScaleX = -1;
                                    imgPolice.RenderTransform = flipTrans;
                                    Grid.SetRow(imgPolice, 0);

                                    TextBlock lbGoToJail = new TextBlock();
                                    Grid.SetRow(lbGoToJail, 2);

                                    gridLayout.Children.Add(imgPolice);
                                    gridLayout.Children.Add(lbGoToJail);

                                    BoardPanelGoToJail.Children.Add(gridLayout);
                                    break;
                                }
                                break;
                        }

                    }

                    switch (index)
                    {
                        case 1:
                            if (c.GetType() == typeof(Land))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetColumn(border, numberOfCellsToInsertInPanel - indexOfNumberOfCellInPanel - 1);

                                Grid gridLayout = new Grid();
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                gridLayout.Tag = globalIndex;

                                Grid gridLayoutText = new Grid();
                                gridLayoutText.RowDefinitions.Add(new RowDefinition());
                                gridLayoutText.RowDefinitions.Add(new RowDefinition());
                                Grid.SetRow(gridLayoutText, 2);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetRow(tbName, 0);

                                BrushConverter converter = new BrushConverter();
                                TextBlock tbColorGroup = new TextBlock();
                                tbColorGroup.Background = (Brush)converter.ConvertFrom(GameManager.Instance.boardHandler.getColor(c));
                                Grid.SetRow(tbColorGroup, 1);

                                TextBlock tbPlayerColor = new TextBlock();
                                Grid.SetRow(tbPlayerColor, 0);

                                gridLayoutText.Children.Add(tbName);
                                gridLayout.Children.Add(gridLayoutText);
                                gridLayout.Children.Add(tbColorGroup);
                                gridLayout.Children.Add(tbPlayerColor);

                                border.Child = gridLayout;
                                BoardPanelBottom.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(TrainStation))
                            {
                                TrainStation trainStation = (TrainStation)c;
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetColumn(border, numberOfCellsToInsertInPanel - indexOfNumberOfCellInPanel - 1);

                                Grid gridLayout = new Grid();
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                gridLayout.Tag = globalIndex;

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetRow(tbName, 1);

                                Image iconTrainStation = new Image();
                                iconTrainStation.Source = Base64Converter.base64ToImageSource(trainStation.Icon);
                                Grid.SetRow(iconTrainStation, 2);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                Grid.SetRow(borderPlayerColor, 0);
                                */
                                Grid.SetRow(tbPlayerColor, 0);

                                gridLayout.Children.Add(tbName);
                                gridLayout.Children.Add(iconTrainStation);
                                gridLayout.Children.Add(tbPlayerColor);

                                border.Child = gridLayout;
                                BoardPanelBottom.Children.Add(border);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(PublicService))
                            {
                                PublicService CellPublicService = (PublicService)c;
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetColumn(border, numberOfCellsToInsertInPanel - indexOfNumberOfCellInPanel - 1);

                                Grid gridLayout = new Grid();
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                gridLayout.Tag = globalIndex;


                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetRow(tbName, 1);

                                Image iconCompany = new Image();
                                iconCompany.Source = Base64Converter.base64ToImageSource(CellPublicService.Icon);
                                Grid.SetRow(iconCompany, 2);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;

                                Grid.SetRow(borderPlayerColor, 0);
                                */
                                Grid.SetRow(tbPlayerColor, 0);

                                gridLayout.Children.Add(tbName);
                                gridLayout.Children.Add(iconCompany);
                                gridLayout.Children.Add(tbPlayerColor);

                                border.Child = gridLayout;
                                BoardPanelBottom.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(Tax))
                            {
                                
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetColumn(border, numberOfCellsToInsertInPanel - indexOfNumberOfCellInPanel - 1);

                                Grid gridLayout = new Grid();
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.Tag = globalIndex;

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetRow(tbName, 0);

                                Image iconTax = new Image();
                                iconTax.Source = Base64Converter.base64ToImageSource(((Tax)c).Icon);
                                Grid.SetRow(iconTax, 1);

                                TextBlock tbPlayerColor = new TextBlock();
                                //Border borderPlayerColor = new Border();
                                //borderPlayerColor.BorderBrush = Brushes.Gray;
                                //borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                //borderPlayerColor.Child = tbPlayerColor;
                                Grid.SetRow(tbPlayerColor, 2);

                                gridLayout.Children.Add(tbName);
                                gridLayout.Children.Add(iconTax);
                                gridLayout.Children.Add(tbPlayerColor);

                                border.Child = gridLayout;
                                BoardPanelBottom.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(DrawCard))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetColumn(border, numberOfCellsToInsertInPanel - indexOfNumberOfCellInPanel - 1);

                                Grid gridLayout = new Grid();
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.Tag = globalIndex;

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetRow(tbName, 0);

                                Image iconDrawCard = new Image();
                                iconDrawCard.Source = Base64Converter.base64ToImageSource(((DrawCard)c).Icon);
                               
                                Grid.SetRow(iconDrawCard, 1);

                                TextBlock tbPlayerColor = new TextBlock();
                                //Border borderPlayerColor = new Border();
                                //borderPlayerColor.BorderBrush = Brushes.Gray;
                                //borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                //borderPlayerColor.Child = tbPlayerColor;
                                Grid.SetRow(tbPlayerColor, 2);

                                gridLayout.Children.Add(tbName);
                                gridLayout.Children.Add(iconDrawCard);
                                gridLayout.Children.Add(tbPlayerColor);

                                border.Child = gridLayout;
                                BoardPanelBottom.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            break;
                        case 2:
                            if (c.GetType() == typeof(Land))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetRow(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();

                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                gridLayout.Tag = globalIndex;

                                Grid gridLayoutText = new Grid();
                                gridLayoutText.ColumnDefinitions.Add(new ColumnDefinition());
                                gridLayoutText.ColumnDefinitions.Add(new ColumnDefinition());
                                Grid.SetColumn(gridLayoutText, 0);

                                RotateTransform rotate = new RotateTransform();
                                rotate.Angle = 90;

                                TransformGroup transform = new TransformGroup();
                                transform.Children.Add(rotate);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.Wrap;
                                tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                                tbName.RenderTransform = transform;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetColumn(tbName, 1);

                                BrushConverter converter = new BrushConverter();
                                TextBlock tbColorGroup = new TextBlock();
                                tbColorGroup.Background = (Brush)converter.ConvertFrom(GameManager.Instance.boardHandler.getColor(c));
                                Grid.SetColumn(tbColorGroup, 1);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetColumn(tbPlayerColor, 2);

                                gridLayoutText.Children.Add(tbName);
                                gridLayout.Children.Add(tbPlayerColor);
                                gridLayout.Children.Add(tbColorGroup);
                                gridLayout.Children.Add(gridLayoutText);

                                border.Child = gridLayout;
                                BoardPanelLeft.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(TrainStation))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetRow(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();

                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                gridLayout.Tag = globalIndex;


                                RotateTransform rotate = new RotateTransform();
                                rotate.Angle = 90;

                                TransformGroup transform = new TransformGroup();
                                transform.Children.Add(rotate);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                                tbName.RenderTransform = transform;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetColumn(tbName, 1);

                                Image iconTrainStation = new Image();
                                iconTrainStation.Source = Base64Converter.base64ToImageSource(((TrainStation)c).Icon);
                                iconTrainStation.RenderTransformOrigin = new Point(0.5, 0.5);
                                iconTrainStation.RenderTransform = transform;
                                Grid.SetColumn(iconTrainStation, 0);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetColumn(tbPlayerColor, 2);


                                gridLayout.Children.Add(tbPlayerColor);
                                gridLayout.Children.Add(iconTrainStation);
                                gridLayout.Children.Add(tbName);

                                border.Child = gridLayout;
                                BoardPanelLeft.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(PublicService))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetRow(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();

                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                gridLayout.Tag = globalIndex;


                                RotateTransform rotate = new RotateTransform();
                                rotate.Angle = 90;

                                TransformGroup transform = new TransformGroup();
                                transform.Children.Add(rotate);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                                tbName.RenderTransform = transform;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetColumn(tbName, 1);

                                Image iconCompany = new Image();
                                iconCompany.Source = Base64Converter.base64ToImageSource(((PublicService)c).Icon); ;
                                iconCompany.RenderTransformOrigin = new Point(0.5, 0.5);
                                iconCompany.RenderTransform = transform;
                                Grid.SetColumn(iconCompany, 0);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetColumn(tbPlayerColor, 2);


                                gridLayout.Children.Add(tbPlayerColor);
                                gridLayout.Children.Add(iconCompany);
                                gridLayout.Children.Add(tbName);

                                border.Child = gridLayout;
                                BoardPanelLeft.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(Tax))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetRow(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();

                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                gridLayout.Tag = globalIndex;


                                RotateTransform rotate = new RotateTransform();
                                rotate.Angle = 90;

                                TransformGroup transform = new TransformGroup();
                                transform.Children.Add(rotate);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                                tbName.RenderTransform = transform;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetColumn(tbName, 1);

                                Image iconTax = new Image();
                                iconTax.Source = Base64Converter.base64ToImageSource(((Tax)c).Icon); ;
                                iconTax.RenderTransformOrigin = new Point(0.5, 0.5);
                                iconTax.RenderTransform = transform;
                                Grid.SetColumn(iconTax, 0);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetColumn(tbPlayerColor, 2);


                                gridLayout.Children.Add(tbPlayerColor);
                                gridLayout.Children.Add(iconTax);
                                gridLayout.Children.Add(tbName);

                                border.Child = gridLayout;
                                BoardPanelLeft.Children.Add(border);

                                indexOfNumberOfCellInPanel++;

                            }
                            else if (c.GetType() == typeof(DrawCard))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetRow(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();

                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                
                                gridLayout.Tag = globalIndex;


                                RotateTransform rotate = new RotateTransform();
                                rotate.Angle = 90;

                                TransformGroup transform = new TransformGroup();
                                transform.Children.Add(rotate);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                                tbName.RenderTransform = transform;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetColumn(tbName, 1);

                                Image iconDrawCard = new Image();
                                BitmapImage bitmapIcon = new BitmapImage();
                                iconDrawCard.Source = Base64Converter.base64ToImageSource(((DrawCard)c).Icon); ; ;
                                iconDrawCard.RenderTransformOrigin = new Point(0.5, 0.5);
                                iconDrawCard.RenderTransform = transform;
                                Grid.SetColumn(iconDrawCard, 0);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetColumn(tbPlayerColor, 2);


                                gridLayout.Children.Add(tbPlayerColor);
                                gridLayout.Children.Add(iconDrawCard);
                                gridLayout.Children.Add(tbName);

                                border.Child = gridLayout;
                                BoardPanelLeft.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            break;
                        case 3:
                            if (c.GetType() == typeof(Land))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetColumn(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.Tag = globalIndex;

                                Grid gridLayoutText = new Grid();
                                gridLayoutText.RowDefinitions.Add(new RowDefinition());
                                gridLayoutText.RowDefinitions.Add(new RowDefinition());
                                Grid.SetRow(gridLayoutText, 0);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetRow(tbName, 0);

                                BrushConverter converter = new BrushConverter();
                                TextBlock tbColorGroup = new TextBlock();
                                tbColorGroup.Background = (Brush)converter.ConvertFrom(GameManager.Instance.boardHandler.getColor(c));
                                Grid.SetRow(tbColorGroup, 1);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetRow(tbPlayerColor, 2);

                                gridLayoutText.Children.Add(tbName);
                                gridLayout.Children.Add(gridLayoutText);
                                gridLayout.Children.Add(tbColorGroup);
                                gridLayout.Children.Add(tbPlayerColor);

                                border.Child = gridLayout;
                                BoardPanelTop.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(TrainStation))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetColumn(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.Tag = globalIndex;

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetRow(tbName, 0);

                                Image iconTrainStation = new Image();
                                iconTrainStation.Source = Base64Converter.base64ToImageSource(((TrainStation)c).Icon); ;
                                Grid.SetRow(iconTrainStation, 1);

                                TextBlock tbPlayerColor = new TextBlock();
                                //Border borderPlayerColor = new Border();
                                //borderPlayerColor.BorderBrush = Brushes.Gray;
                                //borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                //borderPlayerColor.Child = tbPlayerColor;
                                Grid.SetRow(tbPlayerColor, 2);

                                gridLayout.Children.Add(tbName);
                                gridLayout.Children.Add(iconTrainStation);
                                gridLayout.Children.Add(tbPlayerColor);

                                border.Child = gridLayout;
                                BoardPanelTop.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(PublicService))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetColumn(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.Tag = globalIndex;

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetRow(tbName, 0);

                                Image iconCompany = new Image();                               
                                iconCompany.Source = Base64Converter.base64ToImageSource(((PublicService)c).Icon); ; ;
                                Grid.SetRow(iconCompany, 1);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetRow(tbPlayerColor, 2);

                                gridLayout.Children.Add(tbName);
                                gridLayout.Children.Add(iconCompany);
                                gridLayout.Children.Add(tbPlayerColor);

                                border.Child = gridLayout;
                                BoardPanelTop.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(Tax))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetColumn(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.Tag = globalIndex;

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetRow(tbName, 0);

                                Image iconTax = new Image();
                                iconTax.Source = Base64Converter.base64ToImageSource(((Tax)c).Icon); ; ;
                                Grid.SetRow(iconTax, 1);

                                TextBlock tbPlayerColor = new TextBlock();
                                //Border borderPlayerColor = new Border();
                                //borderPlayerColor.BorderBrush = Brushes.Gray;
                                //borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                //borderPlayerColor.Child = tbPlayerColor;
                                Grid.SetRow(tbPlayerColor, 2);

                                gridLayout.Children.Add(tbName);
                                gridLayout.Children.Add(iconTax);
                                gridLayout.Children.Add(tbPlayerColor);

                                border.Child = gridLayout;
                                BoardPanelTop.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(DrawCard))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetColumn(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(70) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                                gridLayout.Tag = globalIndex;


                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetRow(tbName, 0);

                                Image iconDrawCard = new Image();
                                iconDrawCard.Source = Base64Converter.base64ToImageSource(((DrawCard)c).Icon); ; ;
                                Grid.SetRow(iconDrawCard, 1);

                                TextBlock tbPlayerColor = new TextBlock();
                                //Border borderPlayerColor = new Border();
                                //borderPlayerColor.BorderBrush = Brushes.Gray;
                                //borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                //borderPlayerColor.Child = tbPlayerColor;
                                Grid.SetRow(tbPlayerColor, 2);

                                gridLayout.Children.Add(tbName);
                                gridLayout.Children.Add(iconDrawCard);
                                gridLayout.Children.Add(tbPlayerColor);

                                border.Child = gridLayout;
                                BoardPanelTop.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }

                            break;
                        case 4:
                            if (c.GetType() == typeof(Land))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetRow(border, indexOfNumberOfCellInPanel);


                                Grid gridLayout = new Grid();
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                gridLayout.Tag = globalIndex;

                                Grid gridLayoutText = new Grid();
                                gridLayoutText.ColumnDefinitions.Add(new ColumnDefinition());
                                gridLayoutText.ColumnDefinitions.Add(new ColumnDefinition());
                                Grid.SetColumn(gridLayoutText, 2);

                                RotateTransform rotate = new RotateTransform();
                                rotate.Angle = 270;

                                TransformGroup transform = new TransformGroup();
                                transform.Children.Add(rotate);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.Wrap;
                                tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                                tbName.RenderTransform = transform;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetColumn(tbName, 0);

                                BrushConverter converter = new BrushConverter();
                                TextBlock tbColorGroup = new TextBlock();
                                tbColorGroup.Background = (Brush)converter.ConvertFrom(GameManager.Instance.boardHandler.getColor(c));
                                Grid.SetColumn(tbColorGroup, 1);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetColumn(tbPlayerColor, 0);

                                gridLayoutText.Children.Add(tbName);
                                gridLayout.Children.Add(gridLayoutText);
                                gridLayout.Children.Add(tbColorGroup);
                                gridLayout.Children.Add(tbPlayerColor);

                                border.Child = gridLayout;
                                BoardPanelRight.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(TrainStation))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetRow(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                gridLayout.Tag = globalIndex;

                                RotateTransform rotate = new RotateTransform();
                                rotate.Angle = 270;

                                TransformGroup transform = new TransformGroup();
                                transform.Children.Add(rotate);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                                tbName.RenderTransform = transform;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetColumn(tbName, 1);

                                Image iconTrainStation = new Image();
                                iconTrainStation.Source = Base64Converter.base64ToImageSource(((TrainStation)c).Icon); ; ;
                                iconTrainStation.RenderTransformOrigin = new Point(0.5, 0.5);
                                iconTrainStation.RenderTransform = transform;
                                Grid.SetColumn(iconTrainStation, 2);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetColumn(tbPlayerColor, 1);


                                gridLayout.Children.Add(tbPlayerColor);
                                gridLayout.Children.Add(iconTrainStation);
                                gridLayout.Children.Add(tbName);

                                border.Child = gridLayout;
                                BoardPanelRight.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(PublicService))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetRow(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                
                                gridLayout.Tag = globalIndex;


                                RotateTransform rotate = new RotateTransform();
                                rotate.Angle = 90;

                                TransformGroup transform = new TransformGroup();
                                transform.Children.Add(rotate);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                                tbName.RenderTransform = transform;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetColumn(tbName, 1);

                                Image iconCompany = new Image();
                                iconCompany.Source = Base64Converter.base64ToImageSource(((PublicService)c).Icon); ; ;
                                iconCompany.RenderTransformOrigin = new Point(0.5, 0.5);
                                iconCompany.RenderTransform = transform;
                                Grid.SetColumn(iconCompany, 2);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetColumn(tbPlayerColor, 0);


                                gridLayout.Children.Add(tbPlayerColor);
                                gridLayout.Children.Add(iconCompany);
                                gridLayout.Children.Add(tbName);

                                border.Child = gridLayout;
                                BoardPanelRight.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(Tax))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetRow(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();

                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                
                                gridLayout.Tag = globalIndex;


                                RotateTransform rotate = new RotateTransform();
                                rotate.Angle = 270;

                                TransformGroup transform = new TransformGroup();
                                transform.Children.Add(rotate);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                                tbName.RenderTransform = transform;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetColumn(tbName, 1);

                                Image iconTax = new Image();
                                iconTax.Source = Base64Converter.base64ToImageSource(((Tax)c).Icon); ; ;
                                iconTax.RenderTransformOrigin = new Point(0.5, 0.5);
                                iconTax.RenderTransform = transform;
                                Grid.SetColumn(iconTax, 2);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetColumn(tbPlayerColor, 0);


                                gridLayout.Children.Add(tbPlayerColor);
                                gridLayout.Children.Add(iconTax);
                                gridLayout.Children.Add(tbName);

                                border.Child = gridLayout;
                                BoardPanelRight.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(DrawCard))
                            {
                                Border border = new Border();
                                border.BorderBrush = Brushes.Gray;
                                border.BorderThickness = new Thickness(1);
                                Grid.SetRow(border, indexOfNumberOfCellInPanel);

                                Grid gridLayout = new Grid();
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                                gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                
                                gridLayout.Tag = globalIndex;


                                RotateTransform rotate = new RotateTransform();
                                rotate.Angle = 270;

                                TransformGroup transform = new TransformGroup();
                                transform.Children.Add(rotate);

                                TextBlock tbName = new TextBlock();
                                tbName.VerticalAlignment = VerticalAlignment.Center;
                                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                                tbName.TextAlignment = TextAlignment.Center;
                                tbName.TextWrapping = TextWrapping.WrapWithOverflow;
                                tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                                tbName.RenderTransform = transform;
                                tbName.Text = c.Title;
                                tbName.FontSize = 9;
                                tbName.Padding = new Thickness(5);
                                Grid.SetColumn(tbName, 1);

                                Image iconDrawCard = new Image();
                                BitmapImage bitmapIcon = new BitmapImage();
                                iconDrawCard.Source = Base64Converter.base64ToImageSource(((DrawCard)c).Icon); ; ;
                                iconDrawCard.RenderTransformOrigin = new Point(0.5, 0.5);
                                iconDrawCard.RenderTransform = transform;

                                Grid.SetColumn(iconDrawCard, 2);

                                TextBlock tbPlayerColor = new TextBlock();
                                /*
                                Border borderPlayerColor = new Border();
                                borderPlayerColor.BorderBrush = Brushes.Gray;
                                borderPlayerColor.BorderThickness = new Thickness(1);
                                //borderPlayerColor.CornerRadius = new CornerRadius(0, 0, 5, 5);
                                borderPlayerColor.Child = tbPlayerColor;
                                */
                                Grid.SetColumn(tbPlayerColor, 0);


                                gridLayout.Children.Add(tbPlayerColor);
                                gridLayout.Children.Add(iconDrawCard);
                                gridLayout.Children.Add(tbName);

                                border.Child = gridLayout;
                                BoardPanelRight.Children.Add(border);

                                indexOfNumberOfCellInPanel++;
                            }
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Le nombre de case est incorrect");
            }
        }
        #endregion
    }
}
