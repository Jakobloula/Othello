using System;
using System.Collections.Generic;

namespace Othello.GameModel
{
    public class GameBoard
    {
        public Disk[,] board;
        public event Action<Move?, Disk>? DiskFlipped;

        public GameBoard()
        {
            board = new Disk[8, 8];   
        }
        
        
        //skapar board
        public void InitializeBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    board[row, col] = Disk.Empty;
                }
            }
            board[4, 3] = board[3, 4] = Disk.Black;//player1 start pjäser
            board[4, 4] = board[3, 3] = Disk.White;//player2 start pjäser
        }

        public List<Move> GetValidMoves(Disk player)
        {
            List<Move> validMoves = new List<Move>();
            for (int row = 0; row < 8; row++) // 0,1,2,3,4,5,6,7
            {
                for (int col = 0; col < 8; col++) // 0,1,2,3,4,5,6,7
                {
                    Move move = new Move(row, col);
                    if (IsMoveValid(move, player))
                    {
                        validMoves.Add(new Move(row, col));
                    }
                }
            }
            return validMoves;
        }


        public int CountPlayerPieces(Disk player)
        {
            int count = 0;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (board[row, col] == player)
                    {
                        count++;
                    }
                }
            }
            return count;
        }




        //kollar om ett drag är tillåtet på en given possition för en given spelare.
        public bool IsMoveValid(Move? move, Disk player)
        {
            if (move == null)
            {
                return false;
            }

            int? col = move?.Col;
            int? row = move?.Row;
            Disk opponent = (player == Disk.Black) ? Disk.White : Disk.Black;

            if (row < 0 || col < 0 || row >= 8 || col >= 8) //Wadafaak?!
            {
                return false;
            }

            // Check if the space is empty
            if (board[row.Value, col.Value] != Disk.Empty)
            {
                return false;
            }

            bool isValid = false;
            //r=x och c=y 
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    // Skip checking the same position
                    if (dr == 0 && dc == 0)
                    {
                        continue;
                    }

                    int r = row.Value + dr, c = col.Value + dc;
                    bool foundOpponentPiece = false;

                    // Loop while within bounds
                    while (r >= 0 && r < 8 && c >= 0 && c < 8)  // Adjusted bounds based on 0-7 indexing
                    {
                        if (board[r, c] == Disk.Empty)
                        {
                            break;
                        }
                        else if (board[r, c] == player)
                        {
                            if (foundOpponentPiece)
                            {
                                return true;

                            }
                            break;
                        }
                        else if (board[r, c] == opponent) // found opponent piece
                        {
                            foundOpponentPiece = true;
                        }

                        r += dr;
                        c += dc;
                    }
                }
            }
            return false; // Return false if no valid moves found
        }




        //kollar om spelare har några drag kvar
        public bool HasValidMoves(Disk player)
        {
            if (GetValidMoves(player).Count <= 0)
            {
                return false;
            } else
            {
                return true;
            }

        }

        public bool MakeMove(Move? move, Disk player)
        {
            if (!IsMoveValid(move, player)) 
            {
                return false;
            }

            int[] mr = { -1, -1, 0, 1, 1, 1, 0, -1 }; // Row direction
            int[] mc = { 0, 1, 1, 1, 0, -1, -1, -1 }; // Column direction

            int? row = move?.Row;
            int? col = move?.Col;

            if (!row.HasValue || !col.HasValue)
            {
                return false; // Handle invalid move gracefully
            }

            int actualrow = row.Value;
            int actualcol = col.Value;
             

            board[actualrow, actualcol] = player; // Place the disk

            for (int i = 0; i < 8; i++)
            {
                int r =  actualrow+ mr[i];
                int c = actualcol + mc[i];
                bool foundOpponentPiece = false;

                while (r >= 0 && r < 8 && c >= 0 && c < 8)
                {
                    if (board[r, c] == Disk.Empty)
                    {
                        break;
                    }
                    else if (board[r, c] == player)
                    {
                        if (foundOpponentPiece)
                        {
                            int cf = actualcol + mc[i];
                            int rf = actualrow + mr[i];
                            while (cf != c || rf != r)
                            {
                                board[rf, cf] = player;
                                DiskFlipped?.Invoke(new Move(rf, cf), player);
                                cf += mc[i];
                                rf += mr[i];
                            }
                            break;
                        }
                        else { break; }
                    }
                    else
                    {
                        foundOpponentPiece = true;
                    }

                    r += mr[i];
                    c += mc[i];
                }
            }

            
            return true;
        }



    }
}