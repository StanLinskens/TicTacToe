using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacChessSlin
{
    public partial class manual: Form
    {
        int BoardStartX = 25;
        int BoardStartY = 50;
        string grid = "5x5";
        int gap = 14;

        private BoardTile[,] boardGrid; // 2D array to store tiles
        private List<ChessPiece> displayPieces = new List<ChessPiece>(); // Pieces in the UI display

        public manual()
        {
            InitializeComponent();
        }

        private void manual_Load(object sender, EventArgs e)
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

        public void BoardCreation(string gridSize)
        {
            // Convert string to row and column
            string[] sizeParts = gridSize.Split('x'); // Expecting format like "5x5"
            if (sizeParts.Length != 2 || !int.TryParse(sizeParts[0], out int gridRow) || !int.TryParse(sizeParts[1], out int gridCol))
            {
                Console.WriteLine("Invalid grid size format. Use format like '5x5'.");
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

                    // Determine spawn zones (first and last row are spawns)
                    string spawn = "None";
                    if (row == 0) spawn = "White";
                    if (row == gridRow - 1) spawn = "Black";

                    // Determine win condition target tiles (corners of a 3x3 in the 5x5)
                    bool isTargetTile = (row == 1 && (col == 1 || col == 3)) || (row == 3 && (col == 1 || col == 3));
                    string tileType = isTargetTile ? "Target" : "Normal";

                    Image tileImage = null;
                    if (isTargetTile)
                    {
                        tileImage = Properties.Resources.button;
                        //newPanel.BackColor = Color.Gold;
                    }

                    this.Controls.Add(newPanel);

                    boardGrid[row, col] = new BoardTile(newPanel, tileName, tileID++, row, col, spawn, null, tileType, tileImage);
                    boardGrid[row, col].ApplyTileImage();
                }
            }

            Console.WriteLine($"Board of size {gridRow}x{gridCol} created with target tiles for win condition.");
        }

        private void CreateChessPieces()
        {
            Dictionary<string, (Image, bool, List<MoveInstruction>)> pieces = new Dictionary<string, (Image, bool, List<MoveInstruction>)>
            {
                {
                    "Wizard", (Properties.Resources.wizard, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, 1, true)
                        })
                },
                {
                    "Witch", (Properties.Resources.witch, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, 1, true)
                        })
                },
                {
                    "Crossbow", (Properties.Resources.crossbow, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(0, 1, true)
                        })
                },
                {
                    "Inferno_Tower", (Properties.Resources.inferno_tower, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(0, 1, true)
                        })
                },
                {
                    "Fire_Spirit", (Properties.Resources.fire_spirit, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(1, 0, false)
                        })
                },
                {
                    "Electro_Spirit", (Properties.Resources.electro_spirit, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(1, 0, false)
                        })
                },
                {
                    "Prince", (Properties.Resources.prince, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, 2, false)
                        })
                },
                {
                    "Dark_Knight", (Properties.Resources.dark_knight, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, 2, false)
                        })
                },
                {
                    "Baby_Dragon", (Properties.Resources.baby_dragon, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true)
                        })
                },
                {
                    "Electro_Dragon", (Properties.Resources.electro_dragon, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true)
                        })
                },
                {
                    "Dagger_Dutchess", (Properties.Resources.dagger_dutchess, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, 1, true),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(0, 1, true),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true)
                        })
                },
                {
                    "Chef", (Properties.Resources.chef, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, 1, true),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(0, 1, true),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true)
                        })
                },
                {
                    "Ice_Wizard", (Properties.Resources.ice_wizard, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(1, 0, false),
                            new MoveInstruction(1, 1, false)
                        })
                },
                {
                    "Electro_Wizard", (Properties.Resources.electro_wizard, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(1, 0, false),
                            new MoveInstruction(1, 1, false)
                        })
                },
                {
                    "Executioner", (Properties.Resources.executioner, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, -2, false),
                            new MoveInstruction(-2, 2, false),
                            new MoveInstruction(2, -2, false),
                            new MoveInstruction(2, 2, false)
                        })
                },
                {
                    "Valkyrie", (Properties.Resources.valkyrie, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, -2, false),
                            new MoveInstruction(-2, 2, false),
                            new MoveInstruction(2, -2, false),
                            new MoveInstruction(2, 2, false)
                        })
                },
                {
                    "Golem", (Properties.Resources.golem, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(1, 0, false),
                            new MoveInstruction(1, 1, false)
                        })
                },
                {
                    "Pekka", (Properties.Resources.pekka, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(1, 0, false),
                            new MoveInstruction(1, 1, false)
                        })
                },
                {
                    "Hog_Rider", (Properties.Resources.hog_rider, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, 0, false),
                            new MoveInstruction(2, 0, false),
                            new MoveInstruction(0, -2, false),
                            new MoveInstruction(0, 2, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, 2, false)
                        })
                },
                {
                    "Ram_Rider", (Properties.Resources.ram_rider, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, 0, false),
                            new MoveInstruction(2, 0, false),
                            new MoveInstruction(0, -2, false),
                            new MoveInstruction(0, 2, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, 2, false)
                        })
                },
                {
                    "Inferno_Dragon", (Properties.Resources.inferno_dragon, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -3, false),
                            new MoveInstruction(0, 3, false),
                            new MoveInstruction(-3, 0, false),
                            new MoveInstruction(3, 0, false),
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false)
                        })
                },
                {
                    "Phoenix", (Properties.Resources.phoenix, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -3, false),
                            new MoveInstruction(0, 3, false),
                            new MoveInstruction(-3, 0, false),
                            new MoveInstruction(3, 0, false),
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false)
                        })
                },
                {
                    "Mega_Knight", (Properties.Resources.mega_knight, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-3, 0, false),
                            new MoveInstruction(3, 0, false),
                            new MoveInstruction(0, -3, false),
                            new MoveInstruction(0, 3, false),
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false)
                        })
                },
                {
                    "Sparky", (Properties.Resources.sparky, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(1, 0, false),
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(0, -4, true),
                            new MoveInstruction(0, 4, true),
                            new MoveInstruction(-4, 0, true),
                            new MoveInstruction(4, 0, true)
                        })
                }
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

                ChessPiece newPiece = new ChessPiece(piece.Key, piece.Value.Item2, piecePanel, 0, 0, piece.Value.Item3);
                displayPieces.Add(newPiece);

                piecePanel.MouseClick += Piece_MouseClick;


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

        private List<BoardTile> GetValidMoves(ChessPiece piece)
        {
            List<BoardTile> validMoves = new List<BoardTile>();

            foreach (var rule in piece.MovementRules) // ✅ Now correctly using MovementRules
            {
                int newRow = piece.Row;
                int newCol = piece.Col;

                while (true)
                {
                    newRow += rule.RowChange;
                    newCol += rule.ColChange;

                    BoardTile tile = GetTileAt(newRow, newCol);

                    // Stop if out of bounds or occupied
                    if (tile == null || tile.PieceOnTile != null)
                        break;

                    validMoves.Add(tile);

                    // Stop if movement is not infinite (like a Knight or Pawn)
                    if (!rule.IsInfinite)
                        break;
                }
            }

            return validMoves;
        }

        private BoardTile GetTileAt(int row, int col)
        {
            if (row >= 0 && row < boardGrid.GetLength(0) && col >= 0 && col < boardGrid.GetLength(1))
            {
                return boardGrid[row, col];
            }
            return null; // Out of bounds
        }

    }
}