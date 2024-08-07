using System;
using System.Collections.Generic;
using System.Threading;

namespace Othello.GameModel
{
    public class ComputerPlayer : Player
    {
        
        
        public Disk colorDisk { get; set; }

        public ComputerPlayer(Disk colorDisk) : base(colorDisk)
        {

        }
        public override Move? RequestMove(GameBoard board, List<Move> moves)
        {
            Random random = new Random();
            int index = random.Next(0, moves.Count);

            Move move = moves[index];
            Thread.Sleep(500);
            board.MakeMove(move, colorDisk);
            return move;
        }

        public override void SetMove(Move? move)
        {
            throw new NotImplementedException();
        }
    }
}
