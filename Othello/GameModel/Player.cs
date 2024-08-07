using System.Collections.Generic;

namespace Othello.GameModel
{
    public abstract class Player
    {
        public Disk ColorDisk { get; set; }

        public Player(Disk colorDisk)
        {
            ColorDisk = colorDisk;
        }

        public abstract Move? RequestMove(GameBoard board, List<Move> moves);

        public abstract void SetMove(Move? move);
    }
}
