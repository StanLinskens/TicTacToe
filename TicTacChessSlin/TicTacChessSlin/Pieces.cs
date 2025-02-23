using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacChessSlin
{
    internal class ChessPiece
    {
        public string PieceName { get; set; }
        public bool IsWhite { get; set; }
        public Image PieceImage { get; set; }
        public Panel PiecePanel { get; set; }

        public int Row { get; set; }
        public int Col { get; set; }

        public List<MoveInstruction> MovementRules { get; }

        public ChessPiece(string pieceName, bool isWhite, Panel piecePanel, int row, int col, List<MoveInstruction> movementRules)
        {
            PieceName = pieceName;
            IsWhite = isWhite;
            PiecePanel = piecePanel;
            Row = row;
            Col = col;
            MovementRules = movementRules ?? new List<MoveInstruction>();
        }

        private Point GetGridPosition(int row, int col)
        {
            int tileSize = 80;
            return new Point(col * tileSize, row * tileSize);
        }

        // Move the piece to a new row/column
        public void MoveTo(int newRow, int newCol)
        {
            // Check boundaries (3x3 grid)
            if (newRow < 0 || newRow >= 3 || newCol < 0 || newCol >= 3)
            {
                MessageBox.Show("Invalid move! Stay within the grid.");
                return;
            }

            // Update position
            Row = newRow;
            Col = newCol;
            PiecePanel.Location = GetGridPosition(newRow, newCol);
        }
    }
}