namespace Othello.GameModel
{
    public struct Move
    {
        public int? Row;
        public int? Col;

        public Move(int? row, int? col)
        {
            Row = row; Col = col;
        }

        public Move Clone()
        {
            return new Move(Row, Col);
        }
    }
}
