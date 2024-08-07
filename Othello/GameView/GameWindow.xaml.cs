using Othello.GameController;
using Othello.GameModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace Othello.GameView
{


    public partial class GameWindow : Window
    {
       

        private int Ply1;
        private int Ply2;
        private GameBoard gameboard;
        private GameManager gameManager;
       

        public GameWindow(string PlayerOneName, string PlayerTwoName, PlayerType playerType1, PlayerType playerType2)
        {   
            InitializeComponent();
            gameboard = new GameBoard();
            
            
            lblname2.Content = PlayerOneName;
            lblname1.Content = PlayerTwoName;
            gameManager = new GameManager(gameboard, playerType2, playerType1, PlayerOneName, PlayerTwoName);
            
            Ply1 = 2;
            Ply2 = 2;
            
            gameboard.DiskFlipped += OnDiskFlipped;
            gameManager.DiskFlipped += OnDiskFlipped;
            gameManager.OnUpdateLabel += OnUpdateLabel;
            
        }


        private void NewGameBTN (object sender, EventArgs e)
        {
            this.Close();

            Window1 stwindow = new Window1();
            stwindow.Show();
        }

        private void ExitBTN (object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            Button? clickedbutton = sender as Button;
            if (clickedbutton != null)
            {
                int row = Grid.GetRow(clickedbutton);
                int column = Grid.GetColumn(clickedbutton);

                
                Move move = new Move(row, column);
                gameManager.SetMove(move);

                if (gameboard.IsMoveValid(move, gameManager.currentPlayer.ColorDisk))
                {
                    
                    if (gameManager.currentPlayer.ColorDisk == Disk.Black)
                    {
                        Ellipse Black = new Ellipse
                        {
                            Width = 65,
                            Height = 65,
                            Fill = Brushes.Black
                        };

                        clickedbutton.Content = Black;

                    }
                    else
                    {
                        Ellipse whiet = new Ellipse
                        {
                            Width = 65,
                            Height = 65,
                            Fill = Brushes.White
                        };
                        clickedbutton.Content = whiet;
                    }


                    
                }
                
            }
        }

        public Button? FindButtonAt(int? row, int? col)
        {
            Button? result = null;
            Dispatcher.Invoke(() =>
            {
                foreach (UIElement element in Mygrid.Children)
                {
                    if (Grid.GetRow(element) == row && Grid.GetColumn(element) == col && element is Button button)
                    {
                        result = button;
                        break;
                    }
                }
            });
            return result;
        }

        public void OnDiskFlipped(Move? move, Disk disk)
        {
            int? row = move.Value.Row;
            int? col = move.Value.Col;

            Dispatcher.Invoke(() =>
            {
                Button? button = FindButtonAt(row.Value, col.Value);

                if (button != null)
                {
                    Ellipse? ellipse = button.Content as Ellipse;
                    if (ellipse == null)
                    {
                        ellipse = new Ellipse { Width = 65, Height = 65 };
                        button.Content = ellipse;
                    }
                    ellipse.Fill = (disk == Disk.Black) ? Brushes.Black : Brushes.White;
                }
            });
        }

        private void OnUpdateLabel(int ply1score, int ply2score)
        {
            Dispatcher.Invoke(() =>
            {
                Ply1lbl.Content = ply1score.ToString();
                Ply2lbl.Content = ply2score.ToString();
            });
        }

    }
}
