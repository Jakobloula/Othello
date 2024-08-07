using Othello.GameModel;
using Othello.GameView;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace Othello.GameController
{
    public class GameManager
    {
        public GameBoard gameBoard;
        public Player playerOne, playerTwo, currentPlayer;
        public string NameOne, NameTwo;
        private Thread thread;
        public event Action<Move?, Disk>? DiskFlipped;
        public delegate void UpdateLabel(int ply1score, int ply2score);
        public event UpdateLabel OnUpdateLabel;

        public GameManager(GameBoard board, PlayerType playerOneType,PlayerType playerTwoType, string player1name, string player2name)
        {
            if (playerOneType == PlayerType.Human)
            {
                playerOne = new HumanPlayer(Disk.Black);
            } else
            {
                playerOne = new ComputerPlayer(Disk.Black);
            }
            
            if (playerTwoType == PlayerType.Human)
            {
                playerTwo = new HumanPlayer(Disk.White);
            } else
            {
                playerTwo = new ComputerPlayer(Disk.White);
            }

            

            this.NameTwo = player2name; this.NameOne = player1name;
            
            this.gameBoard = board;


            
            currentPlayer = playerOne;
            thread = new Thread(RunGameLoop);
            thread.Start();
            thread.IsBackground = true;

           
        }

        public void UpdateScore()
        {
            int pl1score = gameBoard.CountPlayerPieces(GameModel.Disk.Black);
            int pl2score = gameBoard.CountPlayerPieces(GameModel.Disk.White);

            OnUpdateLabel?.Invoke(pl1score, pl2score);
        }
        

        public string GetWinner(Disk player1, Disk player2)
        {
            //Måste anpassa koden efter vilket namn man väljer eller om det dator.

            int countPlayer1 = gameBoard.CountPlayerPieces(player1);
            int countPlayer2 = gameBoard.CountPlayerPieces(player2);
            


            if (countPlayer1 > countPlayer2)
            {
                return "Winner player 1"; //måste anpassa
            }
            else if (countPlayer1 < countPlayer2)
            {
                return "Winner Player 2"; //måste anpassa
            }
            else
            {
                return "Draw";
            }
        }

        public void Switchplayer()
        {
            if (currentPlayer == playerOne)
            {
                currentPlayer = playerTwo;
            }
            else
            {
                currentPlayer = playerOne;
            }
        }


        public void RunGameLoop()
        {

            gameBoard.InitializeBoard();
            
            while (true)
            {
                UpdateScore();


                if (IsGameOver() == true)
                {
                    break;
                }

                List<Move> validmoves = gameBoard.GetValidMoves(currentPlayer.ColorDisk);
                if (validmoves.Count > 0)
                {
                    Move? move = currentPlayer.RequestMove(gameBoard, validmoves);
                    if (gameBoard.IsMoveValid(move, currentPlayer.ColorDisk))
                    {
                        gameBoard.MakeMove(move, currentPlayer.ColorDisk);
                        DiskFlipped?.Invoke(move, currentPlayer.ColorDisk);
                        Switchplayer();
                    }
                
                }
                else
                {
                    Switchplayer();
                }
                
            }
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show("GameOver!");
                string winner = GetWinner(playerOne.ColorDisk, playerTwo.ColorDisk);
                WinnerDialog dialog = new WinnerDialog(winner);
                dialog.ShowDialog();
            });

        }


        public void SetMove(Move move)
        {
            currentPlayer.SetMove(move);
        }



        public bool IsGameOver()
        {
            
            if (gameBoard.HasValidMoves(Disk.Black) == false && gameBoard.HasValidMoves(Disk.White) == false)
            {
                return true;
            }

            // Annars är spelet inte över
            return false;
        }
        

    }
}
