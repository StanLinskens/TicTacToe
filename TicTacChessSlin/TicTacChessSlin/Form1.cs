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

            boardGrid = new BoardTile[gridRow, gridCol];

            int tileID = 1;
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
                        BoardStartX + col * (newPanel.Width + gap),
                        BoardStartY + row * (newPanel.Height + gap)
                    );

                    // Determine spawn zones (assuming the first and last rows are spawn points)
                    string spawn = "None";
                    if (row == 0) spawn = "White";
                    if (row == gridRow - 1) spawn = "Black";

                    
                    this.Controls.Add(newPanel);

                    boardGrid[row, col] = new BoardTile(newPanel, tileName, tileID++, row, col, spawn);
                }
            }

            Console.WriteLine($"Board of size {gridRow}x{gridCol} created.");
        }


        private void CreateChessPieces()
        {
            Dictionary<string, (Image, bool, string)> pieces = new Dictionary<string, (Image, bool, string)>
            {
                { "Wizard", (Properties.Resources.wizard, true, "upLeft+, upRight+, downLeft+, downRight+") },
                { "Witch", (Properties.Resources.witch, false, "upLeft+, upRight+, downLeft+, downRight+") },
                { "Prince", (Properties.Resources.prince, true, "up2-left1, up2-right1, down2-left1, down2-right1, left2-up1, left2-down1, right2-up1, right2-down1") },
                { "Dark_Knight", (Properties.Resources.dark_knight, false, "up2-left1, up2-right1, down2-left1, down2-right1, left2-up1, left2-down1, right2-up1, right2-down1") },
                { "Inferno_Tower", (Properties.Resources.inferno_tower, true, "left+, right+, up+, down+") },
                { "Crossbow", (Properties.Resources.crossbow, false, "left+, right+, up+, down+") },
                { "Baby_Dragon", (Properties.Resources.baby_dragon, true, "left1, right1, up+, down+") },
                { "Electro_Dragon", (Properties.Resources.electro_dragon, false, "left1, right1, up+, down+") },
                { "Fire_Spirit", (Properties.Resources.fire_spirit, true, "left1, right1, up1, down1") },
                { "Electro_Spirit", (Properties.Resources.electro_spirit, false, "left1, right1, up1, down1") },
                { "Chef", (Properties.Resources.chef, true, "up+, upRight+, right+, downRight+, down+, downLeft+, left+, upLeft+") },
                { "Dagger_Dutchess", (Properties.Resources.dagger_dutchess, false, "up+, upRight+, right+, downRight+, down+, downLeft+, left+, upLeft+") }
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

                displayPieces.Add(newPiece);

                piecePanel.MouseDown += Piece_MouseDown;
                piecePanel.MouseUp += Piece_MouseUp;
                piecePanel.MouseMove += Piece_MouseMove;

                PieceLibrary.AddOrUpdatePiece(piece.Key, newPiece);
            }
        }

        private void DisplayPieces(bool showTruePieces)
        {
            int x = 2;
            int y = 18;
            int spacingX = 85;
            int spacingY = 85;
            int columns = 2;

            int count = 0;

            foreach (var piece in displayPieces)
            {
                if (piece.IsWhite == showTruePieces)
                {
                    piece.PiecePanel.Enabled = true;
                    piece.PiecePanel.Visible = true;
                    piece.PiecePanel.Location = new Point(x, y);
                    gbxPiecesHolder.Controls.Add(piece.PiecePanel);

                    count++;

                    if (count % columns == 0)  // Move to the next row after filling a column
                    {
                        x = 5;
                        y += spacingY;
                    }
                    else
                    {
                        x += spacingX;
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

                // Split combined moves (e.g., "upLeft3-left2" or "up+")
                string[] moveParts = move.Split('-');


                bool infiniteMovement = false; // Track if it's an infinite movement

                foreach (string part in moveParts)
                {
                    int step = 1; // Default step if no number is provided
                    string processedPart = part; // Create a modifiable copy

                    // Check if movement is infinite (has '+')
                    if (processedPart.EndsWith("+"))
                    {
                        infiniteMovement = true;
                        processedPart = processedPart.TrimEnd('+'); // Remove '+' for parsing
                    }

                    // Extract number from move, if available
                    string number = new string(processedPart.Where(char.IsDigit).ToArray());
                    if (!string.IsNullOrEmpty(number))
                    {
                        step = int.Parse(number);
                        processedPart = new string(processedPart.Where(c => !char.IsDigit(c)).ToArray()); // Remove digits
                    }

                    // Apply movement
                    int dRow = 0, dCol = 0;
                    switch (part)
                    {
                        case "left": dCol = -1; break;
                        case "right": dCol = 1; break;
                        case "up": dRow = -1; break;
                        case "down": dRow = 1; break;
                        case "upLeft": dRow = -1; dCol = -1; break;
                        case "upRight": dRow = -1; dCol = 1; break;
                        case "downLeft": dRow = 1; dCol = -1; break;
                        case "downRight": dRow = 1; dCol = 1; break;
                    }

                    // Apply movement loop (for infinite movement)
                    while (true)
                    {
                        newRow += dRow * step;
                        newCol += dCol * step;

                        // Stop if out of bounds
                        if (newRow < 0 || newRow >= 3 || newCol < 0 || newCol >= 3)
                            break;

                        BoardTile targetTile = boardGrid[newRow, newCol];

                        // Stop if the path is blocked
                        if (targetTile.PieceOnTile != "None")
                            break;

                        // Highlight the tile
                        boardGrid[newRow, newCol].TilePanel.BackColor = Color.LightGreen;

                        // If not infinite, stop after one move
                        if (!infiniteMovement)
                            break;
                    }
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

            // Get the mouse position relative to the form
            Point mousePosition = selectedPiece.PiecePanel.Parent.PointToClient(Cursor.Position);

            BoardTile hoveredTile = null;
            foreach (var tile in boardGrid)
            {
                Rectangle tileBounds = new Rectangle(tile.TilePanel.Location, tile.TilePanel.Size);
                if (tileBounds.Contains(mousePosition))
                {
                    hoveredTile = tile;
                    break;
                }
            }

            if (hoveredTile != null && hoveredTile.PieceOnTile == "None") // Check if tile is empty
            {
                if (!ActiveGame)
                {
                    if ((selectedPiece.IsWhite && hoveredTile.Spawn == "White") ||
                        (!selectedPiece.IsWhite && hoveredTile.Spawn == "Black"))
                    {
                        MovePieceToTile(selectedPiece, hoveredTile);
                    }
                    else
                    {
                        ResetPiecePosition();
                    }
                }
                else
                {
                    MovePieceToTile(selectedPiece, hoveredTile);
                }
            }
            else
            {
                ResetPiecePosition();
            }

            ResetAllTileColors();
            selectedPiece = null;
        }

        // Moves a piece to a specific tile and updates tile occupancy
        private void MovePieceToTile(ChessPiece piece, BoardTile tile)
        {
            piece.PiecePanel.Location = tile.TilePanel.Location;
            piece.Row = tile.Row;
            piece.Col = tile.Col;

            tile.PieceOnTile = piece.ToString(); // Assign piece to tile

            if (!ActiveGame)
            {
                displayPieces.Remove(piece);
                boardPieces.Add(piece);
            }
        }

        // Resets the piece back to its original position if placement fails
        private void ResetPiecePosition()
        {
            if (selectedPiece != null)
            {
                selectedPiece.PiecePanel.Location = pieceOriginalPosition;
            }
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