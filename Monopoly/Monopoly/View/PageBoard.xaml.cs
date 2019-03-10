using MaterialDesignThemes.Wpf;
using Monopoly.Handlers;
using Monopoly.Models.Components;
using Monopoly.Models.Components.Cells;
using Monopoly.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Monopoly.View
{
    /// <summary>
    /// Logique d'interaction pour PageBoard.xaml
    /// </summary>
    public partial class PageBoard : Page
    {

        #region Variables
        private GameManager gameManager = GameManager.Instance;
        private enum CellOrientation : int { BOTTUM = 0, LEFT = 1, TOP = 2, RIGHT = 3 };
        private enum GridType : int { GRIDTYPE_ROW = 1, GRIDTYPE_COLUMN = 2 };
        private const int sizeofElipse = 20;
        private const string START_POSITION = "playerPosition0";
        public Player currentPlayer;
        public string NamePlayer { get { return currentPlayer.Name; } }
        public string test { get { return currentPlayer.Pawn.ColorValue; } }

        //threads
        #endregion

        public PageBoard()
        {
            InitializeComponent();
            this.DataContext = this;
            gameManager.StartGame();
            InitialiseBoard();
            GeneratePlayer();
            currentPlayer = gameManager.playerHandler.GetCurrentPlayer();
            PropertiesListInterface.buildingBought += BuildingBought;
            SellPropertiesListInterface.buildingBought += BuildingBought;
        }

        #region Creation du Plateau

        /// <summary>
        /// Define the GridColumnDefinitions and GridRowDefinitions foreach panel in view
        /// </summary>
        /// <param name="panel">Panel</param>
        /// <param name="numOfDefinition">Iteration of row/column</param>
        /// <param name="type">Row or Column</param>
        private void InitialisePanel(Grid panel, int numOfDefinition, int type)
        {
            switch (type)
            {
                case (int)GridType.GRIDTYPE_COLUMN:
                    int nbCol = 0;
                    while (nbCol < numOfDefinition)
                    {
                        ColumnDefinition col = new ColumnDefinition();
                        panel.ColumnDefinitions.Add(col);
                        nbCol++;
                    }
                    break;
                case (int)GridType.GRIDTYPE_ROW:
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
                InitialisePanel(BoardPanelTop, numberOfCellsToInsertInPanel, (int)GridType.GRIDTYPE_COLUMN);
                InitialisePanel(BoardPanelRight, numberOfCellsToInsertInPanel, (int)GridType.GRIDTYPE_ROW);
                InitialisePanel(BoardPanelLeft, numberOfCellsToInsertInPanel, (int)GridType.GRIDTYPE_ROW);
                InitialisePanel(BoardPanelBottom, numberOfCellsToInsertInPanel, (int)GridType.GRIDTYPE_COLUMN);

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
                                    gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                                    gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                                    gridLayout.Name = "Cell" + ((StartPoint)c).Id;
                                    NameScope.GetNameScope(this).RegisterName("Cell" + ((StartPoint)c).Id, gridLayout);

                                    gridLayout.MouseEnter += Cells_MouseEnter;
                                    gridLayout.MouseLeave += Cells_MouseLeave;

                                    Image imgStart = new Image();
                                    imgStart.Source = Base64Converter.base64ToImageSource(((StartPoint)c).Icon);
                                    Grid.SetRow(imgStart, 1);
                                    Grid.SetColumn(imgStart, 1);

                                    Grid GridPlayerPosition = new Grid();
                                    GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
                                    GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
                                    GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
                                    NameScope.GetNameScope(this).RegisterName("playerPosition" + ((StartPoint)c).Id, GridPlayerPosition);
                                    Grid.SetRow(GridPlayerPosition, 1);
                                    Grid.SetColumn(GridPlayerPosition, 1);

                                    GeneratePlayerElipse(GridPlayerPosition);


                                    gridLayout.Children.Add(imgStart);
                                    gridLayout.Children.Add(GridPlayerPosition);

                                    BoardPanelStart.Children.Add(gridLayout);
                                    break;
                                }
                                break;
                            case 2:
                                if (c.GetType() == typeof(Jail))
                                {
                                    Grid gridLayout = new Grid();
                                    gridLayout.ShowGridLines = true;
                                    gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                                    gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
                                    NameScope.GetNameScope(this).RegisterName("Cell" + ((Jail)c).Id, gridLayout);
                                    gridLayout.Name = "Cell" + ((Jail)c).Id;
                                    gridLayout.MouseEnter += Cells_MouseEnter;
                                    gridLayout.MouseLeave += Cells_MouseLeave;

                                    RotateTransform rotate = new RotateTransform();
                                    rotate.Angle = 45;

                                    TransformGroup transform = new TransformGroup();
                                    transform.Children.Add(rotate);

                                    Image imgJail = new Image();
                                    imgJail.Source = Base64Converter.base64ToImageSource(((Jail)c).Icon);
                                    imgJail.RenderTransformOrigin = new Point(0.5, 0.5);
                                    imgJail.RenderTransform = transform;
                                    Grid.SetRow(imgJail, 0);
                                    Grid.SetColumn(imgJail, 1);

                                    Grid GridPlayerPositionJail = new Grid();
                                    GridPlayerPositionJail.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPositionJail.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPositionJail.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPositionJail.ColumnDefinitions.Add(new ColumnDefinition());
                                    GridPlayerPositionJail.ColumnDefinitions.Add(new ColumnDefinition());
                                    GridPlayerPositionJail.ColumnDefinitions.Add(new ColumnDefinition());
                                    NameScope.GetNameScope(this).RegisterName("playerPosition" + ((Jail)c).Id, GridPlayerPositionJail);

                                    GeneratePlayerElipse(GridPlayerPositionJail);

                                    GridPlayerPositionJail.VerticalAlignment = VerticalAlignment.Center;
                                    GridPlayerPositionJail.HorizontalAlignment = HorizontalAlignment.Center;

                                    Grid.SetRow(GridPlayerPositionJail, 0);
                                    Grid.SetColumn(GridPlayerPositionJail, 1);

                                    Grid GridPlayerPositionVisiteLeft = new Grid();
                                    GridPlayerPositionVisiteLeft.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPositionVisiteLeft.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPositionVisiteLeft.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPositionVisiteLeft.RowDefinitions.Add(new RowDefinition());
                                    NameScope.GetNameScope(this).RegisterName("VisitePositionLeft" + ((Jail)c).Id, GridPlayerPositionVisiteLeft);
                                    Grid.SetRow(GridPlayerPositionVisiteLeft, 0);
                                    Grid.SetColumn(GridPlayerPositionVisiteLeft, 0);

                                    Grid GridPlayerPositionVisiteButtom = new Grid();
                                    GridPlayerPositionVisiteButtom.ColumnDefinitions.Add(new ColumnDefinition());
                                    GridPlayerPositionVisiteButtom.ColumnDefinitions.Add(new ColumnDefinition());
                                    GridPlayerPositionVisiteButtom.ColumnDefinitions.Add(new ColumnDefinition());
                                    GridPlayerPositionVisiteButtom.ColumnDefinitions.Add(new ColumnDefinition());
                                    NameScope.GetNameScope(this).RegisterName("VisitePositionButtom" + ((Jail)c).Id, GridPlayerPositionVisiteButtom);
                                    Grid.SetRow(GridPlayerPositionVisiteButtom, 1);
                                    Grid.SetColumn(GridPlayerPositionVisiteButtom, 1);

                                    gridLayout.Children.Add(imgJail);
                                    gridLayout.Children.Add(GridPlayerPositionJail);
                                    gridLayout.Children.Add(GridPlayerPositionVisiteLeft);
                                    gridLayout.Children.Add(GridPlayerPositionVisiteButtom);

                                    BoardPanelJail.Children.Add(gridLayout);
                                    break;
                                }
                                break;
                            case 3:
                                if (c.GetType() == typeof(Parking))
                                {
                                    Grid gridLayout = new Grid();
                                    gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                                    gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                                    NameScope.GetNameScope(this).RegisterName("Cell" + ((Parking)c).Id, gridLayout);
                                    gridLayout.Name = "Cell" + ((Parking)c).Id;
                                    gridLayout.MouseEnter += Cells_MouseEnter;
                                    gridLayout.MouseLeave += Cells_MouseLeave;

                                    Image imgParking = new Image();
                                    imgParking.Source = Base64Converter.base64ToImageSource(((Parking)c).Icon);

                                    imgParking.RenderTransformOrigin = new Point(0.5, 0.5);
                                    ScaleTransform flipTrans = new ScaleTransform();
                                    imgParking.RenderTransform = flipTrans;
                                    Grid.SetRow(imgParking, 1);
                                    Grid.SetColumn(imgParking, 1);

                                    Grid GridPlayerPosition = new Grid();
                                    GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
                                    GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
                                    GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
                                    NameScope.GetNameScope(this).RegisterName("playerPosition" + ((Parking)c).Id, GridPlayerPosition);
                                    GeneratePlayerElipse(GridPlayerPosition);

                                    Grid.SetRow(GridPlayerPosition, 1);
                                    Grid.SetColumn(GridPlayerPosition, 1);

                                    gridLayout.Children.Add(imgParking);
                                    gridLayout.Children.Add(GridPlayerPosition);

                                    BoardPanelParking.Children.Add(gridLayout);
                                    break;
                                }
                                break;
                            case 4:
                                if (c.GetType() == typeof(GoToJail))
                                {
                                    Grid gridLayout = new Grid();
                                    gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                                    gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                                    gridLayout.Name = "Cell" + ((GoToJail)c).Id;
                                    NameScope.GetNameScope(this).RegisterName("Cell" + ((GoToJail)c).Id, gridLayout);

                                    gridLayout.MouseEnter += Cells_MouseEnter;
                                    gridLayout.MouseLeave += Cells_MouseLeave;

                                    Image imgPolice = new Image();
                                    imgPolice.Source = Base64Converter.base64ToImageSource(((GoToJail)c).Icon);

                                    imgPolice.RenderTransformOrigin = new Point(0.5, 0.5);
                                    ScaleTransform flipTrans = new ScaleTransform();
                                    flipTrans.ScaleX = -1;
                                    imgPolice.RenderTransform = flipTrans;
                                    Grid.SetRow(imgPolice, 1);
                                    Grid.SetColumn(imgPolice, 1);

                                    Grid GridPlayerPosition = new Grid();
                                    GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
                                    GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
                                    GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
                                    GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
                                    NameScope.GetNameScope(this).RegisterName("playerPosition" + ((GoToJail)c).Id, GridPlayerPosition);
                                    Grid.SetRow(GridPlayerPosition, 1);
                                    Grid.SetColumn(GridPlayerPosition, 1);

                                    GeneratePlayerElipse(GridPlayerPosition);

                                    gridLayout.Children.Add(imgPolice);
                                    gridLayout.Children.Add(GridPlayerPosition);

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
                                int position = (numberOfCellsToInsertInPanel - indexOfNumberOfCellInPanel - 1);
                                GenerateLand(((Land)c), CellOrientation.BOTTUM, position, globalIndex);
                                indexOfNumberOfCellInPanel++;

                                /*StackPanel stackProperties = new StackPanel();
                                PackIcon house = new PackIcon { Kind = PackIconKind.House };
                                PackIcon motel = new PackIcon { Kind = PackIconKind.HouseModern };

                                for (int i = 0; i < 4; i++)
                                {
                                     Label l = new Label();
                                     l.Content = house;
                                     stackProperties.Children.Add(l);

                                    PackIcon house = new PackIcon { Kind = PackIconKind.House };

                                    stackProperties.Children.Add(motel);
                                }

                                stackProperties.Children.Add(motel);
                                Grid.SetRow(stackProperties, 1);

                                MaterialDesignThemes.Wpf.Icon icon = 

                                PackIcon house = new PackIcon { Kind = PackIconKind.House };                                
                                PackIcon motel = new PackIcon { Kind = PackIconKind.HouseModern };
                                gridProperties.Children.Add(house);
                                gridProperties.Children.Add(motel);*/

                            }
                            else if (c.GetType() == typeof(TrainStation))
                            {
                                int position = (numberOfCellsToInsertInPanel - indexOfNumberOfCellInPanel - 1);
                                GenerateTrainStation(((TrainStation)c), CellOrientation.BOTTUM, position, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(PublicService))
                            {
                                int position = (numberOfCellsToInsertInPanel - (indexOfNumberOfCellInPanel % numberOfCellsToInsertInPanel) - 1);
                                GeneratePublicService(((PublicService)c), CellOrientation.BOTTUM, position, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(Tax))
                            {
                                int position = (numberOfCellsToInsertInPanel - (indexOfNumberOfCellInPanel % numberOfCellsToInsertInPanel) - 1);
                                GenerateTax(((Tax)c), CellOrientation.BOTTUM, position, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(DrawCard))
                            {
                                int position = (numberOfCellsToInsertInPanel - (indexOfNumberOfCellInPanel % numberOfCellsToInsertInPanel) - 1);
                                GenerateDrawCard(((DrawCard)c), CellOrientation.BOTTUM, position, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            break;

                        case 2:
                            if (c.GetType() == typeof(Land))
                            {
                                int position = (numberOfCellsToInsertInPanel - (indexOfNumberOfCellInPanel % numberOfCellsToInsertInPanel) - 1);
                                GenerateLand(((Land)c), CellOrientation.LEFT, position, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(TrainStation))
                            {
                                int position = (numberOfCellsToInsertInPanel - (indexOfNumberOfCellInPanel % numberOfCellsToInsertInPanel) - 1);
                                GenerateTrainStation(((TrainStation)c), CellOrientation.LEFT, position, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(PublicService))
                            {
                                int position = (numberOfCellsToInsertInPanel - (indexOfNumberOfCellInPanel % numberOfCellsToInsertInPanel) - 1);
                                GeneratePublicService(((PublicService)c), CellOrientation.LEFT, position, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(Tax))
                            {
                                int position = (numberOfCellsToInsertInPanel - (indexOfNumberOfCellInPanel % numberOfCellsToInsertInPanel) - 1);
                                GenerateTax(((Tax)c), CellOrientation.LEFT, position, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(DrawCard))
                            {
                                int position = (numberOfCellsToInsertInPanel - (indexOfNumberOfCellInPanel % numberOfCellsToInsertInPanel) - 1);
                                GenerateDrawCard(((DrawCard)c), CellOrientation.LEFT, position, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            break;
                        case 3:
                            if (c.GetType() == typeof(Land))
                            {
                                GenerateLand(((Land)c), CellOrientation.TOP, indexOfNumberOfCellInPanel, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(TrainStation))
                            {
                                GenerateTrainStation(((TrainStation)c), CellOrientation.TOP, indexOfNumberOfCellInPanel, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(PublicService))
                            {
                                GeneratePublicService(((PublicService)c), CellOrientation.TOP, indexOfNumberOfCellInPanel, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(Tax))
                            {
                                GenerateTax(((Tax)c), CellOrientation.TOP, indexOfNumberOfCellInPanel, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(DrawCard))
                            {
                                GenerateDrawCard(((DrawCard)c), CellOrientation.TOP, indexOfNumberOfCellInPanel, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }

                            break;
                        case 4:
                            if (c.GetType() == typeof(Land))
                            {
                                GenerateLand(((Land)c), CellOrientation.RIGHT, indexOfNumberOfCellInPanel, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(TrainStation))
                            {
                                GenerateTrainStation(((TrainStation)c), CellOrientation.RIGHT, indexOfNumberOfCellInPanel, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(PublicService))
                            {
                                GeneratePublicService(((PublicService)c), CellOrientation.RIGHT, indexOfNumberOfCellInPanel, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(Tax))
                            {
                                GenerateTax(((Tax)c), CellOrientation.RIGHT, indexOfNumberOfCellInPanel, globalIndex);
                                indexOfNumberOfCellInPanel++;
                            }
                            else if (c.GetType() == typeof(DrawCard))
                            {
                                GenerateDrawCard(((DrawCard)c), CellOrientation.RIGHT, indexOfNumberOfCellInPanel, globalIndex);
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

        /// <summary>
        /// Create Land
        /// </summary>
        /// <param name="land"></param>
        /// <param name="orientation"></param>
        /// <param name="position"></param>
        /// <param name="tag"></param>
        private void GenerateLand(Land land, CellOrientation orientation, int position, int tag)
        {
            Border border = new Border();
            border.BorderBrush = Brushes.Gray;
            border.BorderThickness = new Thickness(1);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetColumn(border, position);
                    break;
                case CellOrientation.LEFT:
                    Grid.SetRow(border, position);
                    break;
                case CellOrientation.TOP:
                    Grid.SetColumn(border, position);
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetRow(border, position);
                    break;
                default:
                    break;
            }

            BrushConverter bc = new BrushConverter();

            Grid GridMain = new Grid();
            GridMain.Background = Brushes.Transparent;
            GridMain.Tag = tag;
            GridMain.Name = "Cell" + land.Id;
            NameScope.GetNameScope(this).RegisterName("Cell" + land.Id, GridMain);

            GridMain.MouseEnter += Cells_MouseEnter;
            GridMain.MouseLeave += Cells_MouseLeave;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                    break;
                case CellOrientation.LEFT:
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    break;
                case CellOrientation.TOP:
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    break;
                case CellOrientation.RIGHT:
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                    break;
                default:
                    break;
            }

            Grid GridContent = new Grid();
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(GridContent, 2);
                    break;
                case CellOrientation.LEFT:
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(GridContent, 1);
                    break;
                case CellOrientation.TOP:
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(GridContent, 0);
                    break;
                case CellOrientation.RIGHT:
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(GridContent, 2);
                    break;
                default:
                    break;
            }

            TextBlock tbName = new TextBlock();
            tbName.VerticalAlignment = VerticalAlignment.Center;
            tbName.HorizontalAlignment = HorizontalAlignment.Center;
            tbName.TextAlignment = TextAlignment.Center;
            tbName.TextWrapping = TextWrapping.Wrap;
            tbName.Text = land.Title;
            tbName.FontSize = 9;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbName, 0);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbName.RenderTransform = transformLeft;
                    Grid.SetColumn(tbName, 1);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbName, 0);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbName.RenderTransform = transformRight;
                    Grid.SetColumn(tbName, 0);
                    break;
                default:
                    break;
            }

            BrushConverter converter = new BrushConverter();
            TextBlock tbColorGroup = new TextBlock();
            tbColorGroup.Background = (Brush)converter.ConvertFrom(land.LandGroup.Color);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbColorGroup, 1);
                    break;
                case CellOrientation.LEFT:
                    Grid.SetColumn(tbColorGroup, 2);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbColorGroup, 2);
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetColumn(tbColorGroup, 1);
                    break;
                default:
                    break;
            }

            StackPanel tbPlayerColor = new StackPanel();
            tbPlayerColor.Name = "PlayerColor";
            NameScope.GetNameScope(this).RegisterName("PlayerColor" + land.Id, tbPlayerColor);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbPlayerColor, 0);
                    tbPlayerColor.Orientation = Orientation.Horizontal;
                    break;
                case CellOrientation.LEFT:
                    Grid.SetColumn(tbPlayerColor, 3);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbPlayerColor, 3);
                    tbPlayerColor.Orientation = Orientation.Horizontal;
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetColumn(tbPlayerColor, 0);
                    break;
                default:
                    break;
            }

            TextBlock tbPurchase = new TextBlock();
            tbPurchase.VerticalAlignment = VerticalAlignment.Center;
            tbPurchase.HorizontalAlignment = HorizontalAlignment.Center;
            tbPurchase.TextAlignment = TextAlignment.Center;
            tbPurchase.TextWrapping = TextWrapping.NoWrap;
            tbPurchase.Text = land.PurchasePrice + "€";
            tbPurchase.FontSize = 8;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbPurchase, 3);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    tbPurchase.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbPurchase.RenderTransform = transformLeft;
                    Grid.SetColumn(tbPurchase, 0);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbPurchase, 1);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    tbPurchase.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbPurchase.RenderTransform = transformRight;
                    Grid.SetColumn(tbPurchase, 3);
                    break;
                default:
                    break;
            }


            Grid GridPlayerPosition = new Grid();
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            NameScope.GetNameScope(this).RegisterName("playerPosition" + land.Id, GridPlayerPosition);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(GridPlayerPosition, 2);
                    break;
                case CellOrientation.LEFT:
                    Grid.SetColumn(GridPlayerPosition, 1);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(GridPlayerPosition, 0);
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetColumn(GridPlayerPosition, 2);
                    break;
                default:
                    break;
            }
            GeneratePlayerElipse(GridPlayerPosition);

            GridContent.Children.Add(tbName);
            GridMain.Children.Add(tbColorGroup);
            GridMain.Children.Add(tbPlayerColor);
            GridMain.Children.Add(tbPurchase);
            GridMain.Children.Add(GridContent);
            GridMain.Children.Add(GridPlayerPosition);
            border.Child = GridMain;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    BoardPanelBottom.Children.Add(border);
                    break;
                case CellOrientation.LEFT:
                    BoardPanelLeft.Children.Add(border);
                    break;
                case CellOrientation.TOP:
                    BoardPanelTop.Children.Add(border);
                    break;
                case CellOrientation.RIGHT:
                    BoardPanelRight.Children.Add(border);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Create train station 
        /// </summary>
        /// <param name="trainStation"></param>
        /// <param name="orientation"></param>
        /// <param name="position"></param>
        /// <param name="tag"></param>
        private void GenerateTrainStation(TrainStation trainStation, CellOrientation orientation, int position, int tag)
        {
            Border border = new Border();
            border.BorderBrush = Brushes.Gray;
            border.BorderThickness = new Thickness(1);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetColumn(border, position);
                    break;
                case CellOrientation.LEFT:
                    Grid.SetRow(border, position);
                    break;
                case CellOrientation.TOP:
                    Grid.SetColumn(border, position);
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetRow(border, position);
                    break;
                default:
                    break;
            }

            Grid GridMain = new Grid();
            GridMain.Background = Brushes.Transparent;
            GridMain.Tag = tag;
            GridMain.Name = "Cell" + trainStation.Id;
            NameScope.GetNameScope(this).RegisterName("Cell" + trainStation.Id, GridMain);

            //border.Name = "Cell" + trainStation.Id;
            GridMain.MouseEnter += Cells_MouseEnter;
            GridMain.MouseLeave += Cells_MouseLeave;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                    break;
                case CellOrientation.LEFT:
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    break;
                case CellOrientation.TOP:
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    break;
                case CellOrientation.RIGHT:
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                    break;
                default:
                    break;
            }

            StackPanel tbPlayerColor = new StackPanel();
            tbPlayerColor.Name = "PlayerColor";
            NameScope.GetNameScope(this).RegisterName("PlayerColor" + trainStation.Id, tbPlayerColor);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbPlayerColor, 0);
                    tbPlayerColor.Orientation = Orientation.Horizontal;
                    break;
                case CellOrientation.LEFT:
                    Grid.SetColumn(tbPlayerColor, 3);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbPlayerColor, 3);
                    tbPlayerColor.Orientation = Orientation.Horizontal;
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetColumn(tbPlayerColor, 0);
                    break;
                default:
                    break;
            }
            Grid GridContent = new Grid();
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(GridContent, 2);
                    break;
                case CellOrientation.LEFT:
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(GridContent, 1);
                    break;
                case CellOrientation.TOP:
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(GridContent, 0);
                    break;
                case CellOrientation.RIGHT:
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(GridContent, 2);
                    break;
                default:
                    break;
            }


            TextBlock tbName = new TextBlock();
            tbName.VerticalAlignment = VerticalAlignment.Center;
            tbName.HorizontalAlignment = HorizontalAlignment.Center;
            tbName.TextAlignment = TextAlignment.Center;
            tbName.TextWrapping = TextWrapping.Wrap;
            tbName.Text = trainStation.Title;
            tbName.FontSize = 9;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbName, 0);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbName.RenderTransform = transformLeft;
                    Grid.SetColumn(tbName, 1);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbName, 0);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbName.RenderTransform = transformRight;
                    Grid.SetColumn(tbName, 0);
                    break;
                default:
                    break;
            }

            Image iconTrainStation = new Image();
            iconTrainStation.Source = Base64Converter.base64ToImageSource(trainStation.Icon);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(iconTrainStation, 1);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    iconTrainStation.RenderTransformOrigin = new Point(0.5, 0.5);
                    iconTrainStation.RenderTransform = transformLeft;
                    Grid.SetColumn(iconTrainStation, 0);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(iconTrainStation, 1);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    iconTrainStation.RenderTransformOrigin = new Point(0.5, 0.5);
                    iconTrainStation.RenderTransform = transformRight;
                    Grid.SetColumn(iconTrainStation, 1);
                    break;
                default:
                    break;
            }

            TextBlock tbPurchase = new TextBlock();
            tbPurchase.VerticalAlignment = VerticalAlignment.Center;
            tbPurchase.HorizontalAlignment = HorizontalAlignment.Center;
            tbPurchase.TextAlignment = TextAlignment.Center;
            tbPurchase.TextWrapping = TextWrapping.NoWrap;
            tbPurchase.Text = trainStation.PurchasePrice + "€";
            tbPurchase.FontSize = 8;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbPurchase, 3);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    tbPurchase.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbPurchase.RenderTransform = transformLeft;
                    Grid.SetColumn(tbPurchase, 0);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbPurchase, 1);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    tbPurchase.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbPurchase.RenderTransform = transformRight;
                    Grid.SetColumn(tbPurchase, 3);
                    break;
                default:
                    break;
            }

            Grid GridPlayerPosition = new Grid();
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            NameScope.GetNameScope(this).RegisterName("playerPosition" + trainStation.Id, GridPlayerPosition);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(GridPlayerPosition, 2);
                    break;
                case CellOrientation.LEFT:
                    Grid.SetColumn(GridPlayerPosition, 1);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(GridPlayerPosition, 0);
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetColumn(GridPlayerPosition, 2);
                    break;
                default:
                    break;
            }
            GeneratePlayerElipse(GridPlayerPosition);

            GridContent.Children.Add(tbName);
            GridContent.Children.Add(iconTrainStation);
            GridMain.Children.Add(tbPlayerColor);
            GridMain.Children.Add(GridContent);
            GridMain.Children.Add(GridPlayerPosition);

            border.Child = GridMain;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    BoardPanelBottom.Children.Add(border);
                    break;
                case CellOrientation.LEFT:
                    BoardPanelLeft.Children.Add(border);
                    break;
                case CellOrientation.TOP:
                    BoardPanelTop.Children.Add(border);
                    break;
                case CellOrientation.RIGHT:
                    BoardPanelRight.Children.Add(border);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Create Public Service Cell
        /// </summary>
        /// <param name="publicService"></param>
        /// <param name="orientation"></param>
        /// <param name="position"></param>
        /// <param name="tag"></param>
        private void GeneratePublicService(PublicService publicService, CellOrientation orientation, int position, int tag)
        {
            Border border = new Border();
            border.BorderBrush = Brushes.Gray;
            border.BorderThickness = new Thickness(1);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetColumn(border, position);
                    break;
                case CellOrientation.LEFT:
                    Grid.SetRow(border, position);
                    break;
                case CellOrientation.TOP:
                    Grid.SetColumn(border, position);
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetRow(border, position);
                    break;
                default:
                    break;
            }

            Grid GridMain = new Grid();
            GridMain.Background = Brushes.Transparent;
            GridMain.Tag = tag;

            NameScope.GetNameScope(this).RegisterName("Cell" + publicService.Id, GridMain);
            GridMain.Name = "Cell" + publicService.Id;
            GridMain.MouseEnter += Cells_MouseEnter;
            GridMain.MouseLeave += Cells_MouseLeave;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                    break;
                case CellOrientation.LEFT:
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    break;
                case CellOrientation.TOP:
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    break;
                case CellOrientation.RIGHT:
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                    break;
                default:
                    break;
            }

            StackPanel tbPlayerColor = new StackPanel();
            tbPlayerColor.Name = "PlayerColor";
            NameScope.GetNameScope(this).RegisterName("PlayerColor" + publicService.Id, tbPlayerColor);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbPlayerColor, 0);
                    tbPlayerColor.Orientation = Orientation.Horizontal;
                    break;
                case CellOrientation.LEFT:
                    Grid.SetColumn(tbPlayerColor, 3);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbPlayerColor, 3);
                    tbPlayerColor.Orientation = Orientation.Horizontal;
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetColumn(tbPlayerColor, 0);
                    break;
                default:
                    break;
            }


            Grid GridContent = new Grid();
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(GridContent, 2);
                    break;
                case CellOrientation.LEFT:
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(GridContent, 1);
                    break;
                case CellOrientation.TOP:
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(GridContent, 0);
                    break;
                case CellOrientation.RIGHT:
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(GridContent, 2);
                    break;
                default:
                    break;
            }


            TextBlock tbName = new TextBlock();
            tbName.VerticalAlignment = VerticalAlignment.Center;
            tbName.HorizontalAlignment = HorizontalAlignment.Center;
            tbName.TextAlignment = TextAlignment.Center;
            tbName.TextWrapping = TextWrapping.Wrap;
            tbName.Text = publicService.Title;
            tbName.FontSize = 9;
            //tbName.Padding = new Thickness(3);
            //tbName.Margin = new Thickness(6);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbName, 0);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbName.RenderTransform = transformLeft;
                    Grid.SetColumn(tbName, 1);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbName, 0);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbName.RenderTransform = transformRight;
                    Grid.SetColumn(tbName, 0);
                    break;
                default:
                    break;
            }

            Image iconPublicService = new Image();
            iconPublicService.Source = Base64Converter.base64ToImageSource(publicService.Icon);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(iconPublicService, 1);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    iconPublicService.RenderTransformOrigin = new Point(0.5, 0.5);
                    iconPublicService.RenderTransform = transformLeft;
                    Grid.SetColumn(iconPublicService, 0);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(iconPublicService, 1);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    iconPublicService.RenderTransformOrigin = new Point(0.5, 0.5);
                    iconPublicService.RenderTransform = transformRight;
                    Grid.SetColumn(iconPublicService, 1);
                    break;
                default:
                    break;
            }

            TextBlock tbPurchase = new TextBlock();
            tbPurchase.VerticalAlignment = VerticalAlignment.Center;
            tbPurchase.HorizontalAlignment = HorizontalAlignment.Center;
            tbPurchase.TextAlignment = TextAlignment.Center;
            tbPurchase.TextWrapping = TextWrapping.NoWrap;
            tbPurchase.Text = publicService.PurchasePrice + "€";
            tbPurchase.FontSize = 8;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbPurchase, 3);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    tbPurchase.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbPurchase.RenderTransform = transformLeft;
                    Grid.SetColumn(tbPurchase, 0);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbPurchase, 1);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    tbPurchase.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbPurchase.RenderTransform = transformRight;
                    Grid.SetColumn(tbPurchase, 3);
                    break;
                default:
                    break;
            }

            Grid GridPlayerPosition = new Grid();
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            NameScope.GetNameScope(this).RegisterName("playerPosition" + publicService.Id, GridPlayerPosition);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(GridPlayerPosition, 2);
                    break;
                case CellOrientation.LEFT:
                    Grid.SetColumn(GridPlayerPosition, 1);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(GridPlayerPosition, 0);
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetColumn(GridPlayerPosition, 2);
                    break;
                default:
                    break;
            }
            GeneratePlayerElipse(GridPlayerPosition);

            GridContent.Children.Add(tbName);
            GridContent.Children.Add(iconPublicService);
            GridMain.Children.Add(tbPlayerColor);
            GridMain.Children.Add(GridContent);
            GridMain.Children.Add(GridPlayerPosition);

            border.Child = GridMain;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    BoardPanelBottom.Children.Add(border);
                    break;
                case CellOrientation.LEFT:
                    BoardPanelLeft.Children.Add(border);
                    break;
                case CellOrientation.TOP:
                    BoardPanelTop.Children.Add(border);
                    break;
                case CellOrientation.RIGHT:
                    BoardPanelRight.Children.Add(border);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Create Tax Cell
        /// </summary>
        /// <param name="tax"></param>
        /// <param name="orientation"></param>
        /// <param name="position"></param>
        /// <param name="tag"></param>
        private void GenerateTax(Tax tax, CellOrientation orientation, int position, int tag)
        {
            Border border = new Border();
            border.BorderBrush = Brushes.Gray;
            border.BorderThickness = new Thickness(1);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetColumn(border, position);
                    break;
                case CellOrientation.LEFT:
                    Grid.SetRow(border, position);
                    break;
                case CellOrientation.TOP:
                    Grid.SetColumn(border, position);
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetRow(border, position);
                    break;
                default:
                    break;
            }

            Grid GridMain = new Grid();
            GridMain.Background = Brushes.Transparent;
            GridMain.Tag = tag;
            GridMain.Name = "Cell" + tax.Id;
            NameScope.GetNameScope(this).RegisterName("Cell" + tax.Id, GridMain);

            GridMain.MouseEnter += Cells_MouseEnter;
            GridMain.MouseLeave += Cells_MouseLeave;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                    break;
                case CellOrientation.LEFT:
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    break;
                case CellOrientation.TOP:
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    break;
                case CellOrientation.RIGHT:
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                    break;
                default:
                    break;
            }


            Grid GridContent = new Grid();
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(GridContent, 2);
                    break;
                case CellOrientation.LEFT:
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(GridContent, 1);
                    break;
                case CellOrientation.TOP:
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(GridContent, 0);
                    break;
                case CellOrientation.RIGHT:
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(GridContent, 2);
                    break;
                default:
                    break;
            }


            TextBlock tbName = new TextBlock();
            tbName.VerticalAlignment = VerticalAlignment.Center;
            tbName.HorizontalAlignment = HorizontalAlignment.Center;
            tbName.TextAlignment = TextAlignment.Center;
            tbName.TextWrapping = TextWrapping.Wrap;
            tbName.Text = tax.Title;
            tbName.FontSize = 9;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbName, 0);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbName.RenderTransform = transformLeft;
                    Grid.SetColumn(tbName, 1);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbName, 0);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbName.RenderTransform = transformRight;
                    Grid.SetColumn(tbName, 0);
                    break;
                default:
                    break;
            }

            Image iconTax = new Image();
            iconTax.Source = Base64Converter.base64ToImageSource(tax.Icon);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(iconTax, 1);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    iconTax.RenderTransformOrigin = new Point(0.5, 0.5);
                    iconTax.RenderTransform = transformLeft;
                    Grid.SetColumn(iconTax, 0);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(iconTax, 1);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    iconTax.RenderTransformOrigin = new Point(0.5, 0.5);
                    iconTax.RenderTransform = transformRight;
                    Grid.SetColumn(iconTax, 1);
                    break;
                default:
                    break;
            }

            TextBlock tbPurchase = new TextBlock();
            tbPurchase.VerticalAlignment = VerticalAlignment.Center;
            tbPurchase.HorizontalAlignment = HorizontalAlignment.Center;
            tbPurchase.TextAlignment = TextAlignment.Center;
            tbPurchase.TextWrapping = TextWrapping.NoWrap;
            tbPurchase.Text = tax.Amount + "€";
            tbPurchase.FontSize = 8;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbPurchase, 3);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    tbPurchase.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbPurchase.RenderTransform = transformLeft;
                    Grid.SetColumn(tbPurchase, 0);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbPurchase, 1);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    tbPurchase.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbPurchase.RenderTransform = transformRight;
                    Grid.SetColumn(tbPurchase, 3);
                    break;
                default:
                    break;
            }

            Grid GridPlayerPosition = new Grid();
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            NameScope.GetNameScope(this).RegisterName("playerPosition" + tax.Id, GridPlayerPosition);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(GridPlayerPosition, 2);
                    break;
                case CellOrientation.LEFT:
                    Grid.SetColumn(GridPlayerPosition, 1);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(GridPlayerPosition, 0);
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetColumn(GridPlayerPosition, 2);
                    break;
                default:
                    break;
            }
            GeneratePlayerElipse(GridPlayerPosition);

            GridContent.Children.Add(tbName);
            GridContent.Children.Add(iconTax);
            GridMain.Children.Add(tbPurchase);
            GridMain.Children.Add(GridContent);
            GridMain.Children.Add(GridPlayerPosition);

            border.Child = GridMain;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    BoardPanelBottom.Children.Add(border);
                    break;
                case CellOrientation.LEFT:
                    BoardPanelLeft.Children.Add(border);
                    break;
                case CellOrientation.TOP:
                    BoardPanelTop.Children.Add(border);
                    break;
                case CellOrientation.RIGHT:
                    BoardPanelRight.Children.Add(border);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Create Draw Card cell
        /// </summary>
        /// <param name="drawCard"></param>
        /// <param name="orientation"></param>
        /// <param name="position"></param>
        /// <param name="tag"></param>
        private void GenerateDrawCard(DrawCard drawCard, CellOrientation orientation, int position, int tag)
        {
            Border border = new Border();
            border.BorderBrush = Brushes.Gray;
            border.BorderThickness = new Thickness(1);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetColumn(border, position);
                    break;
                case CellOrientation.LEFT:
                    Grid.SetRow(border, position);
                    break;
                case CellOrientation.TOP:
                    Grid.SetColumn(border, position);
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetRow(border, position);
                    break;
                default:
                    break;
            }

            Grid GridMain = new Grid();
            GridMain.Background = Brushes.Transparent;
            GridMain.Tag = tag;
            GridMain.Name = "Cell" + drawCard.Id;
            NameScope.GetNameScope(this).RegisterName("Cell" + drawCard.Id, GridMain);

            GridMain.MouseEnter += Cells_MouseEnter;
            GridMain.MouseLeave += Cells_MouseLeave;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                    break;
                case CellOrientation.LEFT:
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    break;
                case CellOrientation.TOP:
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    GridMain.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    break;
                case CellOrientation.RIGHT:
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    GridMain.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
                    break;
                default:
                    break;
            }


            Grid GridContent = new Grid();
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(GridContent, 2);
                    break;
                case CellOrientation.LEFT:
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(GridContent, 1);
                    break;
                case CellOrientation.TOP:
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    GridContent.RowDefinitions.Add(new RowDefinition());
                    Grid.SetRow(GridContent, 0);
                    break;
                case CellOrientation.RIGHT:
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    GridContent.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid.SetColumn(GridContent, 2);
                    break;
                default:
                    break;
            }


            TextBlock tbName = new TextBlock();
            tbName.VerticalAlignment = VerticalAlignment.Center;
            tbName.HorizontalAlignment = HorizontalAlignment.Center;
            tbName.TextAlignment = TextAlignment.Center;
            tbName.TextWrapping = TextWrapping.Wrap;
            tbName.Text = drawCard.Title;
            tbName.FontSize = 9;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(tbName, 0);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbName.RenderTransform = transformLeft;
                    Grid.SetColumn(tbName, 1);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(tbName, 0);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    tbName.RenderTransformOrigin = new Point(0.5, 0.5);
                    tbName.RenderTransform = transformRight;
                    Grid.SetColumn(tbName, 0);
                    break;
                default:
                    break;
            }

            Image iconDrawCard = new Image();
            iconDrawCard.Source = Base64Converter.base64ToImageSource(drawCard.Icon);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(iconDrawCard, 1);
                    break;
                case CellOrientation.LEFT:
                    RotateTransform rotate90 = new RotateTransform { Angle = 90 };
                    TransformGroup transformLeft = new TransformGroup();
                    transformLeft.Children.Add(rotate90);
                    iconDrawCard.RenderTransformOrigin = new Point(0.5, 0.5);
                    iconDrawCard.RenderTransform = transformLeft;
                    Grid.SetColumn(iconDrawCard, 0);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(iconDrawCard, 1);
                    break;
                case CellOrientation.RIGHT:
                    RotateTransform rotate270 = new RotateTransform { Angle = 270 };
                    TransformGroup transformRight = new TransformGroup();
                    transformRight.Children.Add(rotate270);
                    iconDrawCard.RenderTransformOrigin = new Point(0.5, 0.5);
                    iconDrawCard.RenderTransform = transformRight;
                    Grid.SetColumn(iconDrawCard, 1);
                    break;
                default:
                    break;
            }

            Grid GridPlayerPosition = new Grid();
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.RowDefinitions.Add(new RowDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            GridPlayerPosition.ColumnDefinitions.Add(new ColumnDefinition());
            NameScope.GetNameScope(this).RegisterName("playerPosition" + drawCard.Id, GridPlayerPosition);
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    Grid.SetRow(GridPlayerPosition, 2);
                    break;
                case CellOrientation.LEFT:
                    Grid.SetColumn(GridPlayerPosition, 1);
                    break;
                case CellOrientation.TOP:
                    Grid.SetRow(GridPlayerPosition, 0);
                    break;
                case CellOrientation.RIGHT:
                    Grid.SetColumn(GridPlayerPosition, 2);
                    break;
                default:
                    break;
            }

            GeneratePlayerElipse(GridPlayerPosition);

            GridContent.Children.Add(tbName);
            GridContent.Children.Add(iconDrawCard);
            GridMain.Children.Add(GridContent);
            GridMain.Children.Add(GridPlayerPosition);

            border.Child = GridMain;
            switch (orientation)
            {
                case CellOrientation.BOTTUM:
                    BoardPanelBottom.Children.Add(border);
                    break;
                case CellOrientation.LEFT:
                    BoardPanelLeft.Children.Add(border);
                    break;
                case CellOrientation.TOP:
                    BoardPanelTop.Children.Add(border);
                    break;
                case CellOrientation.RIGHT:
                    BoardPanelRight.Children.Add(border);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Create Elipse (pawn player)
        /// </summary>
        /// <param name="color"></param>
        /// <param name="rowPosition"></param>
        /// <param name="colPosition"></param>
        /// <returns></returns>
        private Ellipse CreateElipse(string color, int rowPosition, int colPosition)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Height = sizeofElipse;
            ellipse.Width = sizeofElipse;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            ellipse.Stroke = blackBrush;
            SolidColorBrush playerColor = new SolidColorBrush();
            playerColor.Color = (Color)ColorConverter.ConvertFromString(color);
            ellipse.Fill = playerColor;
            ellipse.Visibility = Visibility.Hidden;

            Grid.SetRow(ellipse, rowPosition);
            Grid.SetColumn(ellipse, colPosition);
            return ellipse;
        }

        /// <summary>
        /// Initialise the player position's in board
        /// </summary>
        /// <param name="c">cellule spécifique</param>
        private void GeneratePlayerElipse(Grid gridPlayerPosition)
        {
            foreach (Player p in gameManager.playerHandler.ListOfPlayers)
            {
                gridPlayerPosition.Children.Add(CreateElipse(p.Pawn.ColorValue, p.Pawn.X, p.Pawn.Y));
            }
        }
        #endregion

        #region Buy property
        private void BuyProperty(Property p)
        {
            MessageBoxResult result = MessageBox.Show("Voulez vous acheter ce terrain à " + p.PurchasePrice + " € ?", "Achat", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                PlayerHandler.Instance.BuyProperty(p);
                PropertyBought(currentPlayer);

            }
        }
        #endregion

        #region Buy building
        private void BuyBuilding(Land l)
        {
            //if (PlayerHandler.Instance.CheckIfPlayerOwnAllLandInLandGroup(currentPlayer, l.LandGroup))
            //{
            MessageBoxResult result = MessageBox.Show("Voulez vous acheter un bien ?", "Achat d'un bien", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                PropertiesListInterface propertiesListInterface = new PropertiesListInterface();
                PropertiesListContent.Content = propertiesListInterface;

                BuildingBought(l);
                propertiesListInterface.Visibility = Visibility.Hidden;
            }
            //}
        }
        #endregion


        #region Dices
        /// <summary>
        /// Click rools dices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onClickDices(object sender, RoutedEventArgs e)
        {
            DicesInterface dicesInterface = new DicesInterface();
            DicesContent.Content = dicesInterface;
            DicesContent.Visibility = Visibility.Visible;
            PropertiesListContent.Visibility = Visibility.Hidden;
            Move(currentPlayer, gameManager.FirstDice, gameManager.SecondeDice);

        }
        #endregion

        #region Players
        /// <summary>
        /// Generate the liste of player in right of frame
        /// </summary>
        private void GeneratePlayer()
        {

            int index = 0;
            foreach (Player p in gameManager.playerHandler.ListOfPlayers)
            {
                index++;
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Margin = new Thickness(5);

                BrushConverter bc = new BrushConverter();
                Ellipse playerEllipse = new Ellipse();
                playerEllipse.Margin = new Thickness(0, 0, 10, 0);
                playerEllipse.Fill = (Brush)bc.ConvertFrom(p.Pawn.ColorValue);
                playerEllipse.Width = sizeofElipse;
                playerEllipse.Height = sizeofElipse;

                TextBlock tbPlayerNumber = new TextBlock();
                tbPlayerNumber.VerticalAlignment = VerticalAlignment.Center;
                tbPlayerNumber.Text = string.Format("Player {0} : {1}  = ", index, p.Name);

                TextBlock tbPlayerAmount = new TextBlock();
                tbPlayerAmount.VerticalAlignment = VerticalAlignment.Center;
                tbPlayerAmount.Text = string.Format("CACHING € or $");

                stackPanel.Children.Add(playerEllipse);
                stackPanel.Children.Add(tbPlayerNumber);
                stackPanel.Children.Add(tbPlayerAmount);
                this.StackPanel_ListOfPlayer.Children.Add(stackPanel);

                object cell = MonopolyBoard.FindName(START_POSITION);
                if (cell is Grid)
                {
                    Grid start = (Grid)cell;
                    int i = 0;
                    int j = -1;
                    foreach (UIElement child in start.Children)
                    {
                        child.Visibility = Visibility.Visible;
                    }
                }

            }

        }
        /// <summary>
        /// Move the player to next cell
        /// </summary>
        /// <param name="p">player</param>
        /// <param name="first">first dice</param>
        /// <param name="second">second dice</param>
        void Move(Player p, Dice first, Dice second)
        {
            int currentPosition = p.Position;
            int previousCurrentPosition = currentPosition;
            int nextPosition = gameManager.NextPosition(p, first, second);

            Grid currentPlayerPosition = (Grid)this.FindName("playerPosition" + currentPosition);
            Ellipse currentEllipse = (Ellipse)GetChildren(currentPlayerPosition, p.Pawn.X, p.Pawn.Y);

            Grid nextPlayerPosition = (Grid)this.FindName("playerPosition" + nextPosition);
            Ellipse nextEllipse = (Ellipse)GetChildren(nextPlayerPosition, p.Pawn.X, p.Pawn.Y);

            int index = 0;
            while (currentPosition != nextPosition)
            {
                index++;
                currentEllipse.Visibility = Visibility.Hidden;

                currentPosition = (currentPosition + 1) % gameManager.boardHandler.Board.ListCell.Count;
                currentPlayerPosition = (Grid)this.FindName("playerPosition" + currentPosition);
                currentEllipse = (Ellipse)GetChildren(currentPlayerPosition, p.Pawn.X, p.Pawn.Y);
                currentEllipse.Visibility = Visibility.Visible;

                gameManager.MovePlayerTo(p, currentPosition);
                if (index > 20)
                {
                    break;
                }
            }

            if (previousCurrentPosition > nextPosition)
            {
                gameManager.playerHandler.GetGratification(currentPlayer);

                MessageBox.Show("200 € de gagné !");
            }

            DoCellAction();
        }

        void PropertyBought(Player p)
        {
            int currentPosition = p.Position;
            StackPanel child = (StackPanel)this.FindName("PlayerColor" + currentPosition);
            BrushConverter bc = new BrushConverter();
            child.Background = (Brush)bc.ConvertFrom(p.Pawn.ColorValue);
        }

        void BuildingBought(Land l)
        {
            int position = l.Id;
            StackPanel child = (StackPanel)this.FindName("PlayerColor" + position);
            child.Children.Clear();
            for (int i = 0; i < l.NbHouse; i++)
            {
                child.Children.Add(new PackIcon { Kind = PackIconKind.House });
            }

            for (int i = 0; i < l.NbHotel; i++)
            {
                child.Children.Add(new PackIcon { Kind = PackIconKind.HouseModern });
            }
        }
        #endregion

        #region Next player
        public void onClickNext(object sender, RoutedEventArgs e)
        {
            //PlayerHandler.Instance.GetCurrentPlayer
        }
        #endregion

        #region Events
        private void Cells_MouseEnter(object sender, MouseEventArgs e)
        {
            //get cell's name
            string name = ((Grid)sender).Name;
            string number = Regex.Replace(name, "Cell", string.Empty);

            // cell's id
            int id = Convert.ToInt32(number);

            //get cell's informations
            Cell c = GameManager.Instance.boardHandler.Board.GetCell(id);
            BrushConverter bc = new BrushConverter();

            if (Card.Visibility == Visibility.Visible || Land.Visibility == Visibility.Visible)
            {
                Card.Visibility = Visibility.Hidden;
                Land.Visibility = Visibility.Hidden;
            }

            Card.Opacity = 1;
            LandDescription.Opacity = 1;

            if (c is TrainStation)
            {
                TrainStation t = (TrainStation)GameManager.Instance.boardHandler.Board.ListCell.ElementAt(id);

                Card.Visibility = Visibility.Visible;
                CardIcon.Source = Base64Converter.base64ToImageSource(((TrainStation)c).Icon);
                lblCardTitle.Text = c.Title;
                lblPurchasePriceValue.Content = t.PurchasePrice + " €";
                lblMortgagePriceValue.Content = t.MortgagePrice + " €";
                lblOwnerCardValue.Content = t.OwnerName;

                Property property = (Property)c;
                if (property.status == Property.MORTGAGED)
                {
                    Card.Opacity = 0.5;
                }
                else
                    Card.Opacity = 1;
            }
            else if (c is Land)
            {
                Buildings.Children.Clear();
                Land.Visibility = Visibility.Visible;
                lblLandTitle.Content = c.Title;
                lblLandTitle.Background = (Brush)bc.ConvertFrom(GameManager.Instance.boardHandler.getColor(c));

                Land l = (Land)GameManager.Instance.boardHandler.Board.ListCell.ElementAt(id);
                lblOwnerValue.Content = l.OwnerName;
                lblBuyingPriceValue.Content = l.PurchasePrice + " €";
                lblLandValue.Content = l.RantalList[0] + " €";
                lblHouse1Value.Content = l.RantalList[1] + " €";
                lblHouse2Value.Content = l.RantalList[2] + " €";
                lblHouse3Value.Content = l.RantalList[3] + " €";
                lblHouse4Value.Content = l.RantalList[4] + " €";
                lblMotelValue.Content = l.RantalList[5] + " €";
                lblMortgageValue.Content = l.MortgagePrice + " €";

               Property property = (Property)c;
                if (property.status == Property.MORTGAGED)
                {
                    LandDescription.Opacity = 0.5;
                }
                else
                    Card.Opacity = 1;

                for (int i = 0; i < l.NbHouse; i++)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("/Monopoly;component/Resources/Pictures/house2.png", UriKind.Relative));
                    image.Height = 40;
                    Buildings.Children.Add(image);
                }

                for (int i = 0; i < l.NbHotel; i++)
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("/Monopoly;component/Resources/Pictures/motel2.png", UriKind.Relative));
                    Buildings.Children.Add(image);
                }
            }
            else if (c is PublicService)
            {
                PublicService p = (PublicService)GameManager.Instance.boardHandler.Board.ListCell.ElementAt(id);
                Card.Visibility = Visibility.Visible;

                CardIcon.Source = Base64Converter.base64ToImageSource(((PublicService)c).Icon);
                lblCardTitle.Text = p.Title;
                lblPurchasePriceValue.Content = p.PurchasePrice + " €";
                lblMortgagePriceValue.Content = p.MortgagePrice + " €";

                Property property = (Property)c;
                if (property.status == Property.MORTGAGED)
                    Card.Opacity = 0.5;
                else
                    Card.Opacity = 1;

            }
        }

        private void Cells_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// Execute the action on cell
        /// </summary>
        private void DoCellAction()
        {
            Cell c = gameManager.boardHandler.Board.GetCell(currentPlayer.Position);

            if (c is Property)
            {
                Property p = (Property)c;

                if (p.status == Property.AVAILABLE_ON_SALE)
                {
                    BuyProperty(p);
                    //TODO : remove it
                    /*if (p is Land)
                    {

                        BuyBuilding((Land)p);
                    }*/
                }
                else if (p.status == Property.NOT_AVAILABLE_ON_SALE)
                {
                    if (PlayerHandler.Instance.CheckIfPlayerOwnThisProperty(p))
                    {
                        /*if (p is Land)
                        {

                            BuyBuilding((Land)p);
                        }*/

                    }
                    else
                        PlayerHandler.Instance.PayTheRent(p, gameManager.FirstDice.Value + gameManager.SecondeDice.Value);
                }
            }
            else if (c.GetType() == typeof(Tax))
            {
                MessageBoxResult mb = MessageBox.Show("Vous devez payer la taxe !");
                PlayerHandler.Instance.PayTheTax((Tax) c);
            }
            else if (c.GetType() == typeof(DrawCard))
            {
                MessageBoxResult mb = MessageBox.Show("Vous piochez une carte !");
            }
            else if (c.GetType() == typeof(StartPoint))
            {
                MessageBoxResult mb = MessageBox.Show("Vous etes sur la case départ !");
            }
            else if (c.GetType() == typeof(Jail))
            {
                MessageBoxResult mb = MessageBox.Show("Vous etes en visite !");
            }
            else if (c.GetType() == typeof(GoToJail))
            {
                MessageBoxResult mb = MessageBox.Show("Vous etes en prison !");
            }
            else if (c.GetType() == typeof(Parking))
            {
                MessageBoxResult mb = MessageBox.Show("Vous recevez toute la MONEY !");
                PlayerHandler.Instance.GetParkingMoney();
            }
            else
            {
                MessageBoxResult mb = MessageBox.Show("Non definie !");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the children element of Gird at row and column position
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private static UIElement GetChildren(Grid grid, int row, int column)
        {
            foreach (UIElement child in grid.Children)
            {
                if ((Grid.GetRow(child) == row) && (Grid.GetColumn(child) == column))
                {
                    return child;
                }
            }
            return null;
        }
        #endregion

        private void onClickBuy(object sender, RoutedEventArgs e)
        {
            DicesContent.Visibility = Visibility.Hidden;
            PropertiesListInterface propertiesListInterface = new PropertiesListInterface();
            PropertiesListContent.Visibility = Visibility.Visible;
            PropertiesListContent.Content = propertiesListInterface;
        }

        private void onClickSell(object sender, RoutedEventArgs e)
        {
            DicesContent.Visibility = Visibility.Hidden;
            SellPropertiesListInterface sellPropertiesListInterface = new SellPropertiesListInterface();
            PropertiesListContent.Visibility = Visibility.Visible;
            PropertiesListContent.Content = sellPropertiesListInterface;
        }

        private void onClickMortgage(object sender, RoutedEventArgs e)
        {
            DicesContent.Visibility = Visibility.Hidden;
            MortgagedPropertiesListInterface mortgagedPropertiesListInterface = new MortgagedPropertiesListInterface();
            PropertiesListContent.Visibility = Visibility.Visible;
            PropertiesListContent.Content = mortgagedPropertiesListInterface;
        }

        private void onClickRaiseMortgage(object sender, RoutedEventArgs e)
        {
            DicesContent.Visibility = Visibility.Hidden;
            RaiseMortgagedPropertiesListInterface raiseMortgagedPropertiesListInterface = new RaiseMortgagedPropertiesListInterface();
            PropertiesListContent.Visibility = Visibility.Visible;
            PropertiesListContent.Content = raiseMortgagedPropertiesListInterface;
        }
    }
}
