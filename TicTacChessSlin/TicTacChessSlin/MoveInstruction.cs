namespace TicTacChessSlin
{
    public class MoveInstruction
    {
        public int RowChange { get; }
        public int ColChange { get; }
        public bool IsInfinite { get; }

        public MoveInstruction(int rowChange, int colChange, bool isInfinite)
        {
            RowChange = rowChange;
            ColChange = colChange;
            IsInfinite = isInfinite;
        }
    }

}