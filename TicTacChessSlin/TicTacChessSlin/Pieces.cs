using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacChessSlin
{
    internal class ChessPiece
    {
        public string PieceName { get; set; }  // Name of the piece (Pawn, Knight, etc.)
        public bool IsWhite { get; set; }      // Is the piece white?
        public Image PieceImage { get; set; }  // The image of the piece
        public Panel PiecePanel { get; set; }  // The PiecePanel where the piece is displayed

        public int Row { get; private set; }   // Row position in the grid
        public int Col { get; private set; }   // Column position in the grid

        public ChessPiece(string pieceName, bool isWhite, Panel piecePanel, int row, int col)
        {
            PieceName = pieceName;
            IsWhite = isWhite;
            PiecePanel = piecePanel;
            Row = row;
            Col = col;
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