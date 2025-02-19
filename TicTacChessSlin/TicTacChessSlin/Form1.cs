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
        int gap = 10;

        private BoardTile[,] boardGrid; // 2D array to store tiles

        private List<ChessPiece> chessPieces = new List<ChessPiece>(); // List to store pieces


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
                    newPanel.BackColor = Color.White;

                    // Set the position: top-left panel starts at (BoardStart, BoardStart)
                    newPanel.Location = new Point(
                        BoardStartX + col * (newPanel.Width + gap),  // X position
                        BoardStartY + row * (newPanel.Height + gap)   // Y position
                    );

                    // Add panel to the form
                    this.Controls.Add(newPanel);

                    boardGrid[row, col] = new BoardTile(newPanel, tileName, tileID++, row, col);
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
            Dictionary<string, (Image, bool)> pieces = new Dictionary<string, (Image, bool)>
            {
                { "Wizard", (Properties.Resources.wizard, true) },
                { "Witch", (Properties.Resources.witch, false) },
                { "Prince", (Properties.Resources.prince, true) },
                { "Dark_Knight", (Properties.Resources.dark_knight, false) },
                { "Inferno_Tower", (Properties.Resources.inferno_tower, true) },
                { "Crossbow", (Properties.Resources.crossbow, false) }
            };

            foreach (var piece in pieces)
            {
                Panel piecePanel = new Panel
                {
                    Width = 80,
                    Height = 80,
                    BackgroundImage = piece.Value.Item1,
                    BackgroundImageLayout = ImageLayout.Zoom,
                    Name = piece.Key,
                    Enabled = false,
                };

                this.Controls.Add(piecePanel);

                PieceLibrary.AddPiece(piece.Key, new ChessPiece(piece.Key, piece.Value.Item2, piecePanel, 0, 0));
            }
        }

        private void DisplayPieces(bool showTruePieces)
        {
            int x = 5;
            int y = 25;
            int spacing = 85; // Space between pieces

            foreach (var piece in PieceLibrary.GetAllPieces())
            {
                if (piece.IsWhite == showTruePieces) // Filter based on team
                {
                    piece.PiecePanel.Enabled = true;
                    piece.PiecePanel.Visible = true;
                    piece.PiecePanel.Location = new Point(x, y);
                    gbxPiecesHolder.Controls.Add(piece.PiecePanel);
                    x += spacing;

                    if (x + spacing > gbxPiecesHolder.Width) // Move to next row if needed
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
    }
}
