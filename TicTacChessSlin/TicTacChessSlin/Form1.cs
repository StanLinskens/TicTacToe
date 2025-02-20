using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TicTacChessSlin
{
    public partial class Form1 : Form
    {
        int BoardStartX = 40;
        int BoardStartY = 125;
        string grid = "3x3";
        int gap = 14;

        bool ActiveGame = false;

        private BoardTile[,] boardGrid; // 2D array to store tiles

        private List<ChessPiece> chessPieces = new List<ChessPiece>(); // List to store pieces

        private List<ChessPiece> displayPieces = new List<ChessPiece>(); // Pieces in the UI display
        private List<ChessPiece> boardPieces = new List<ChessPiece>();   // Pieces placed on the board

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BoardCreation(grid);
            CreateChessPieces();

            // Hide all pieces on startup
            foreach (var piece in PieceLibrary.GetAllPieces())
            {
                piece.PiecePanel.Enabled = false;
                piece.PiecePanel.Visible = false;
            }
        }



        // board logic
        public void BoardCreation(string gridSize)
        {
            // Convert string to row and column
            string[] sizeParts = gridSize.Split('x'); // Expecting format like "3x3"
            if (sizeParts.Length != 2 || !int.TryParse(sizeParts[0], out int gridRow) || !int.TryParse(sizeParts[1], out int gridCol))
            {
                Console.WriteLine("Invalid grid size format. Use format like '3x3'.");
                return;
            }

            boardGrid = new BoardTile[gridRow, gridCol]; // Create grid structure

            int tileID = 1; // Unique ID for each tile
            for (int row = 0; row < gridRow; row++)
            {
                for (int col = 0; col < gridCol; col++)
                {
                    string tileName = $"{(char)('A' + row)}{col + 1}"; // Example: A1, B2, C3
                    string PanelName = $"pnl{tileName}";

                    Panel newPanel = new Panel();
                    newPanel.Name = tileName;
                    newPanel.Width = 80;
                    newPanel.Height = 80;
                    newPanel.BackColor = Color.LightGray;

                    // Set the position: top-left panel starts at (BoardStart, BoardStart)
                    newPanel.Location = new Point(
                        BoardStartX + col * (newPanel.Width + gap),  // X position
                        BoardStartY + row * (newPanel.Height + gap)   // Y position
                    );

                    // Determine spawn zones (assuming the first and last rows are spawn points)
                    string spawn = "None";
                    if (row == 0) spawn = "White";  // Top row is white spawn
                    if (row == gridRow - 1) spawn = "Black"; // Bottom row is black spawn

                    // Add panel to the form
                    this.Controls.Add(newPanel);

                    boardGrid[row, col] = new BoardTile(newPanel, tileName, tileID++, row, col, spawn);
                }
            }

            Console.WriteLine($"Board of size {gridRow}x{gridCol} created.");
        }


        private void PiecePlacement(object sender, MouseEventArgs e)
        {
            //all panels that and in Piece.
            //move piece with mouse. 
            //only drop on gridTile


        }

        private void CreateChessPieces()
        {
            Dictionary<string, (Image, bool, string)> pieces = new Dictionary<string, (Image, bool, string)>
            {
                { "Wizard", (Properties.Resources.wizard, true, "upLeft+, upRight+, downLeft+, downRight+") },  // Infinite diagonal movement
                { "Witch", (Properties.Resources.witch, false, "upLeft+, upRight+, downLeft+, downRight+") },  // Same as Wizard
                { "Prince", (Properties.Resources.prince, true, "up2-left1, up2-right1, down2-left1, down2-right1, left2-up1, left2-down1, right2-up1, right2-down1") },  // Fixed L-shape
                { "Dark_Knight", (Properties.Resources.dark_knight, false, "up2-left1, up2-right1, down2-left1, down2-right1, left2-up1, left2-down1, right2-up1, right2-down1") },  // Fixed L-shape
                { "Inferno_Tower", (Properties.Resources.inferno_tower, true, "left+, right+, up+, down+") },  // Infinite horizontal/vertical movement
                { "Crossbow", (Properties.Resources.crossbow, false, "left+, right+, up+, down+") }  // Same as Inferno Tower
            };

            foreach (var piece in pieces)
            {
                Panel piecePanel = new Panel
                {
                    Width = 80,
                    Height = 80,
                    BackgroundImage = piece.Value.Item1,
                    BackColor = Color.Transparent,
                    BackgroundImageLayout = ImageLayout.Zoom,
                    Name = piece.Key,
                    Enabled = false,
                };

                this.Controls.Add(piecePanel);

                // Create the chess piece
                ChessPiece newPiece = new ChessPiece(piece.Key, piece.Value.Item2, piecePanel, 0, 0, piece.Value.Item3);

                // Store the piece in the display list
                displayPieces.Add(newPiece);

                // Add event handlers
                piecePanel.MouseDown += Piece_MouseDown;
                piecePanel.MouseUp += Piece_MouseUp;
                piecePanel.MouseMove += Piece_MouseMove;

                PieceLibrary.AddOrUpdatePiece(piece.Key, newPiece);
            }
        }


        private void DisplayPieces(bool showTruePieces)
        {
            int x = 5;
            int y = 25;
            int spacing = 85;

            foreach (var piece in displayPieces)
            {
                if (piece.IsWhite == showTruePieces)
                {
                    piece.PiecePanel.Enabled = true;
                    piece.PiecePanel.Visible = true;
                    piece.PiecePanel.Location = new Point(x, y);
                    gbxPiecesHolder.Controls.Add(piece.PiecePanel);
                    x += spacing;

                    if (x + spacing > gbxPiecesHolder.Width)
                    {
                        x = 5;
                        y += spacing;
                    }
                }
                else
                {
                    piece.PiecePanel.Enabled = false;
                    piece.PiecePanel.Visible = false;
                }
            }
        }



        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((System.Windows.Forms.RadioButton)sender).Checked)
            {
                bool showTruePieces = ((System.Windows.Forms.RadioButton)sender).Tag.ToString() == "true";
                DisplayPieces(showTruePieces);
            }
        }

        private void ShowValidMoves(ChessPiece piece)
        {
            string[] moves = piece.MoveSet.Split(',');
            int row = piece.Row;
            int col = piece.Col;

            foreach (string move in moves)
            {
                int newRow = row;
                int newCol = col;

                // Split combined moves (e.g., "up1-left2")
                string[] moveParts = move.Split('-');

                bool pathBlocked = false; // Track if movement is blocked

                foreach (string part in moveParts)
                {
                    if (part.StartsWith("left")) newCol -= int.Parse(part.Substring(4));
                    if (part.StartsWith("right")) newCol += int.Parse(part.Substring(5));
                    if (part.StartsWith("up")) newRow -= int.Parse(part.Substring(2));
                    if (part.StartsWith("down")) newRow += int.Parse(part.Substring(4));

                    // Stop checking further if we go out of bounds
                    if (newRow < 0 || newRow >= 3 || newCol < 0 || newCol >= 3)
                    {
                        pathBlocked = true;
                        break;
                    }

                    BoardTile targetTile = boardGrid[newRow, newCol];

                    // If tile is occupied, block further movement
                    if (targetTile.PieceOnTile != "None")
                    {
                        pathBlocked = true;
                        break;
                    }
                }

                // Highlight only if the path isn't blocked
                if (!pathBlocked)
                {
                    boardGrid[newRow, newCol].TilePanel.BackColor = Color.LightGreen;
                }
            }
        }

        private Point pieceOriginalPosition;
        private ChessPiece selectedPiece;
        private bool isDragging = false;

        private void Piece_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Panel piecePanel)
            {
                selectedPiece = PieceLibrary.GetPieceByPanel(piecePanel);
                if (selectedPiece == null) return;

                isDragging = true;
                pieceOriginalPosition = piecePanel.Location;
                piecePanel.Parent = this;
                piecePanel.BringToFront();

                if (!ActiveGame)
                {
                    // Only allow dropping on the correct spawn tiles before game start
                    ValidSpawn(selectedPiece.IsWhite);
                }
                else
                {
                    // Show normal valid moves when the game has started
                    ShowValidMoves(selectedPiece);
                }
            }
        }

        private void Piece_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && selectedPiece != null)
            {
                // Move piece with the mouse
                selectedPiece.PiecePanel.Location = new Point(
                    selectedPiece.PiecePanel.Location.X + e.X - selectedPiece.PiecePanel.Width / 2,
                    selectedPiece.PiecePanel.Location.Y + e.Y - selectedPiece.PiecePanel.Height / 2
                );
            }
        }

        private void Piece_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDragging || selectedPiece == null) return;

            isDragging = false;
            Panel piecePanel = selectedPiece.PiecePanel;
            Point dropPosition = piecePanel.Location;

            BoardTile validTile = null;
            foreach (var tile in boardGrid)
            {
                Rectangle tileBounds = new Rectangle(tile.TilePanel.Location, tile.TilePanel.Size);
                if (tileBounds.Contains(dropPosition))
                {
                    validTile = tile;
                    break;
                }
            }

            if (validTile != null)
            {
                if (!ActiveGame)
                {
                    if ((selectedPiece.IsWhite && validTile.Spawn == "White") ||
                        (!selectedPiece.IsWhite && validTile.Spawn == "Black"))
                    {
                        piecePanel.Location = validTile.TilePanel.Location;
                        selectedPiece.Row = validTile.Row;
                        selectedPiece.Col = validTile.Col;

                        // Move the piece from display list to board list
                        displayPieces.Remove(selectedPiece);
                        boardPieces.Add(selectedPiece);
                    }
                    else
                    {
                        piecePanel.Location = pieceOriginalPosition;
                    }
                }
                else
                {
                    piecePanel.Location = validTile.TilePanel.Location;
                    selectedPiece.Row = validTile.Row;
                    selectedPiece.Col = validTile.Col;
                }
            }
            else
            {
                piecePanel.Location = pieceOriginalPosition;
            }

            // Reset all tile highlights
            ResetAllTileColors();

            selectedPiece = null;
        }

        // Method to reset all tile colors
        private void ResetAllTileColors()
        {
            foreach (var tile in boardGrid)
            {
                tile.ResetTileColor();
            }
        }


        private void ValidSpawn(bool isWhite)
        {
            foreach (BoardTile tile in boardGrid)
            {
                if (isWhite && tile.Spawn == "White")
                {
                    tile.HighlightTile();
                }
                else if (!isWhite && tile.Spawn == "Black")
                {
                    tile.HighlightTile();
                }
            }
        }
    }
}