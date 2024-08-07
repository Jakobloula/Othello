using System.Collections.Generic;
using System.Threading;

namespace Othello.GameModel
{
    public class HumanPlayer : Player
    {
       private object lockObject = new object();
       private Disk colorDisk { get; set; }
       private Move? move = null;


        public HumanPlayer(Disk colorDisk) : base(colorDisk)
        {

        }

        public override Move? RequestMove(GameBoard board, List<Move> moves)
        {
            lock(lockObject)
            {
                Monitor.Wait(lockObject);
                Move? chosenMove = move?.Clone();
                move = null;
                
                return chosenMove;
            }
        }

        public override void SetMove(Move? move)
        {
            lock (lockObject)
            {
                this.move = move;
                Monitor.PulseAll(lockObject);
            }
            
        }
    }
}
